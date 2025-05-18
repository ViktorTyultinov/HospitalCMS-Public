using Hospital.Composition.ServiceCollection;
using Hospital.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.IntegrationTests.Configuration
{
    public class TestServiceProviderFixture
    {
        public IServiceProvider ServiceProvider { get; }
        public TestServiceProviderFixture()
        {
            var services = new ServiceCollection();

            // Add DbContext
            services.AddDbContext<HospitalDbContext>(options =>
                options.UseInMemoryDatabase("TestDb"));
            services.AddHospitalServices();

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}