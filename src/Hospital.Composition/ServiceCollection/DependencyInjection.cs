using Hospital.Domain.Interfaces.BaseInterfaces;
using Hospital.Infrastructure.Persistance.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Hospital.Composition.ServiceCollection;

public static class DependencyInjection
{
    public static IServiceCollection AddHospitalServices(this IServiceCollection services)
    {
        services
            .RegisterRepositories(typeof(IBaseRepository<>).Assembly, typeof(BedRepository).Assembly)
            .AddLogging(builder => builder.AddConsole())
            .AddInfrastructureServices();

        return services;
    }
}