using System.Collections.Concurrent;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;

namespace Hospital.Application;

internal static class RequestHandlerDelegateFactory
{
    private static readonly ConcurrentDictionary<string, Delegate> DelegateCache = new();

    internal static Func<object, object, CancellationToken, Task<object>> GetOrCreate(Type handlerType, Type requestType, Type responseType, ILogger logger)
    {
        var cacheKey = $"{handlerType.FullName}_{requestType.FullName}_{responseType.FullName}";

        if (DelegateCache.TryGetValue(cacheKey, out var cached)) {
            logger.LogInformation($"✅ Using cached delegate for {cacheKey}");
            return (Func<object, object, CancellationToken, Task<object>>)cached;
        }

        logger.LogWarning($"⚠️  Cache miss! Creating new delegate for {cacheKey}");

        var handlerParam = Expression.Parameter(typeof(object), "handler");
        var requestParam = Expression.Parameter(typeof(object), "request");
        var cancellationTokenParam = Expression.Parameter(typeof(CancellationToken), "cancellationToken");

        var typedHandler = Expression.Convert(handlerParam, handlerType);
        var typedRequest = Expression.Convert(requestParam, requestType);

        var method = handlerType.GetMethod("Handle") 
                     ?? throw new InvalidOperationException($"No Handle method on {handlerType.Name}");

        var call = Expression.Call(typedHandler, method, typedRequest, cancellationTokenParam);
        var box = Expression.Call(typeof(RequestHandlerDelegateFactory), nameof(AwaitAndBox), [responseType], call);

        var lambda = Expression.Lambda<Func<object, object, CancellationToken, Task<object>>>(
            box, handlerParam, requestParam, cancellationTokenParam);

        var compiled = lambda.Compile();
        DelegateCache[cacheKey] = compiled;

        return compiled;
    }

    private static async Task<object> AwaitAndBox<T>(Task<T> task)
    {
        return await task ?? throw new ArgumentNullException(nameof(task));
    }
}
