using Hospital.Application.Interfaces;
using Hospital.Application.UseCases.StorageUnit.Commands;
using Hospital.Infrastructure.Persistance;
using Hospital.IntegrationTests.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.IntegrationTests.Tests.StorageUnit.Commands
{
    [Collection("ServiceProvider collection")]
    public class DeleteStorageUnitUseCaseTests(TestServiceProviderFixture fixture)
    {
        private readonly IServiceProvider _serviceProvider = fixture.ServiceProvider;

        [Theory]
        [InlineData("Storage Unit A", "Description A", "Example Address", "Test Department")]
        public async Task Execute_Should_Delete_StorageUnit(string name, string description, string address, string departmentName)
        {
            var id = Guid.NewGuid();

            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();
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

                dbContext.StorageUnits.Add(new Domain.Entities.Locations.StorageUnit
                {
                    Id = id,
                    Name = name,
                    Address = address,
                    Description = description,
                    DepartmentId = department.Id
                });
                dbContext.SaveChanges();
            }

            using (var scope = _serviceProvider.CreateScope())
            {
                var useCase = scope.ServiceProvider.GetRequiredService<IRequestHandler<DeleteStorageUnitCommand, Guid>>();
                var result = await useCase.Handle(new DeleteStorageUnitCommand(id), CancellationToken.None);
                Assert.Equal(id, result);
            }

            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();
                var deleted = dbContext.StorageUnits.AsNoTracking().FirstOrDefault(su => su.Id == id);
                Assert.Null(deleted);

                var department = dbContext.Departments.AsNoTracking().FirstOrDefault(d => d.Name == departmentName);
                Assert.NotNull(department); // Ensure department is not deleted unintentionally
            }
        }
    }
}
