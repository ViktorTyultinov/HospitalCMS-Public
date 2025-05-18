using System.Diagnostics;
using Hospital.Application.Exceptions;
using Hospital.Application.Interfaces;
using Hospital.Application.Policies;
using Microsoft.Extensions.Logging;
using Polly;

namespace Hospital.Application;
public interface IUseCaseExecutor
{
    Task<UseCaseResult<TResponse>> Dispatch<TResponse>(IRequest<TResponse> request, AsyncPolicy? policy = null, CancellationToken cancellationToken = default);
}
public class UseCaseExecutor(IServiceProvider provider, ILogger<UseCaseExecutor> logger) : IUseCaseExecutor
{
    private readonly IServiceProvider _provider = provider;
    private readonly ILogger<UseCaseExecutor> _logger = logger;
    public async Task<UseCaseResult<TResponse>> Dispatch<TResponse>(IRequest<TResponse> request, AsyncPolicy? policy = null, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var useCaseExecutionTimer = Stopwatch.StartNew();
        var requestType = request.GetType();
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResponse));

        // Used for error tracking
        var correlationId = Guid.NewGuid();

        try
        {
            var handler = _provider.GetService(handlerType);
            if (handler == null)
            {
                var ex = new HandlerNotFoundException(handlerType.Name);
                _logger.LogError(ex, $"❌ CORRELATION: {correlationId} : Handler not found. The payload is {request}");
                return UseCaseResult<TResponse>.Failure(ex.Message, correlationId);
            }

            var delegateTimer = Stopwatch.StartNew();
            var handlerDelegate = RequestHandlerDelegateFactory.GetOrCreate(handlerType, requestType, typeof(TResponse), _logger);
            delegateTimer.Stop();

            _logger.LogInformation($"✅ Delegate for {handlerType.Name} created in {delegateTimer.ElapsedMilliseconds} ms or {delegateTimer.Elapsed.Microseconds} µs");

            var policyToUse = policy ?? DefaultHospitalPolicy.GetDefaultPolicy(_logger);

            var result = await policyToUse.ExecuteAsync(async () =>
            {
                return (TResponse)await handlerDelegate(handler, request, cancellationToken);
            });

            if (result is not TResponse typedResult)
            {
                var ex = new InvalidHandlerResultException(requestType.Name);
                _logger.LogError(ex, $"❌ CORRELATION: {correlationId}: Null or invalid handler result. The payload is {request}");
                return UseCaseResult<TResponse>.Failure(ex.Message, correlationId);
            }

            useCaseExecutionTimer.Stop();
            _logger.LogInformation($"✅ {request} handled in {useCaseExecutionTimer.ElapsedMilliseconds} ms");
            return UseCaseResult<TResponse>.Success(typedResult);
        }
        catch (OperationCanceledException ex)
        {
            useCaseExecutionTimer.Stop();
            _logger.LogWarning($"❌ CORRELATION: {correlationId}: {request} was canceled.");
            return UseCaseResult<TResponse>.Failure(ex.Message, correlationId);
        }
        catch (Exception ex)
        {
            useCaseExecutionTimer.Stop();
            _logger.LogCritical(ex, $"❌ CORRELATION: {correlationId}: Error executing handler for {request}.");
            return UseCaseResult<TResponse>.Failure(ex.Message, correlationId);
        }
    }
}
