using System.Reflection;
using Hospital.Domain.Interfaces.BaseInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.Composition.ServiceCollection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterRepositories(this IServiceCollection services, Assembly domainAssembly, Assembly infrastructureAssembly)
    {
        var genericBase = typeof(IBaseRepository<>);

        var interfaces = domainAssembly
            .GetTypes()
            .Where(t =>
                t.IsInterface &&
                t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == genericBase))
            .ToList();

        var implementations = infrastructureAssembly
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract)
            .ToList();

        foreach (var repoInterface in interfaces)
        {
            var implementation = implementations
                .FirstOrDefault(c => repoInterface.IsAssignableFrom(c));

            if (implementation != null)
            {
                services.AddScoped(repoInterface, implementation);
            }
        }

        return services;
    }
}