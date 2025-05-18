using Hospital.Application.Interfaces;
using Hospital.Application.UseCases.StorageUnit.Commands;
using Hospital.Infrastructure.Persistance;
using Hospital.IntegrationTests.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.IntegrationTests.Tests.StorageUnit.Commands
{
    [Collection("ServiceProvider collection")]
    public class AddStorageUnitUseCaseTests(TestServiceProviderFixture fixture)
    {
        private readonly IServiceProvider _serviceProvider = fixture.ServiceProvider;

        [Theory]
        [InlineData("Storage Unit A", "Description A", "Example Address", "Test Department")]
        public async Task Execute_Should_Add_StorageUnit(string name, string description, string address, string departmentName)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();
            var useCase = scope.ServiceProvider.GetRequiredService<IRequestHandler<AddStorageUnitCommand, Guid>>();

            var department = dbContext.Departments.Add(new Domain.Entities.Locations.Department
            {
                Name = departmentName,
                Description = "Test Description",
                Address = "123 Test St",
                HospitalId = Guid.NewGuid(),
                Rooms = [],
                StorageUnits = []
            }).Entity;

            dbContext.SaveChanges();

            var addedId = await useCase.Handle(new AddStorageUnitCommand(name, description, address, department.Id), CancellationToken.None);

            dbContext.SaveChanges();

            var requested = dbContext.StorageUnits.FirstOrDefault(su => su.Id == addedId);

            Assert.NotNull(requested);
            Assert.Equal(name, requested.Name);
            Assert.Equal(description, requested.Description);
            Assert.Equal(department.Id, requested.DepartmentId);

            dbContext.Remove(requested);
            dbContext.Remove(department);
            dbContext.SaveChanges();
        }
    }
}
