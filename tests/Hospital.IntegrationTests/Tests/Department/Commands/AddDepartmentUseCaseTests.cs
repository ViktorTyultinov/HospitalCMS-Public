using Hospital.Application.Interfaces;
using Hospital.Application.UseCases.Department.Commands;
using Hospital.Infrastructure.Persistance;
using Hospital.IntegrationTests.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.IntegrationTests.Tests.Department.Commands
{
    [Collection("ServiceProvider collection")]
    public class AddDepartmentUseCaseTests(TestServiceProviderFixture fixture)
    {
        private readonly IServiceProvider _serviceProvider = fixture.ServiceProvider;

        [Theory]
        [InlineData("Test Department", "Description", "123 Test St")]
        public async Task Execute_Should_Add_Department(string name, string description, string address)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();
            var useCase = scope.ServiceProvider.GetRequiredService<IRequestHandler<AddDepartmentCommand, Guid>>();
            
            dbContext.SaveChanges();

            var added = await useCase.Handle(new AddDepartmentCommand(name, description, address, Guid.NewGuid()), CancellationToken.None);

            dbContext.SaveChanges();

            var requested = dbContext.Departments.FirstOrDefault(d => d.Id == added);

            Assert.NotNull(requested);
            Assert.Equal(name, requested.Name);
            Assert.Equal(description, requested.Description);
            Assert.Equal(address, requested.Address);

            dbContext.Remove(requested);
            dbContext.SaveChanges();
        }
    }
}
