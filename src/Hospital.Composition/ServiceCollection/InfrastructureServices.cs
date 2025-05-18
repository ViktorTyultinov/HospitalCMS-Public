using Hospital.Infrastructure.Persistance;
using Hospital.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Hospital.Application;

namespace Hospital.Composition.ServiceCollection;

public static class InfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUseCaseExecutor, UseCaseExecutor>();

        var useCaseAssembly = typeof(UseCaseExecutor).Assembly;
        var handlerInterfaceType = typeof(IRequestHandler<,>);

        var handlers = useCaseAssembly
            .GetTypes()
            .Where(t => !t.IsAbstract && !t.IsInterface)
            .SelectMany(t => t.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterfaceType)
                .Select(i => new { Handler = t, Interface = i }));

        foreach (var h in handlers)
        {
            services.AddScoped(h.Interface, h.Handler);
        }

        return services;
    }
}