using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Hospital.Application.UseCases.StorageUnit.Queries;
using Hospital.Infrastructure.Persistance;
using Hospital.IntegrationTests.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.IntegrationTests.Tests.StorageUnit.Queries
{
    [Collection("ServiceProvider collection")]
    public class GetStorageUnitListUseCaseTests(TestServiceProviderFixture fixture)
    {
        private readonly IServiceProvider _serviceProvider = fixture.ServiceProvider;

        [Theory]
        [InlineData(3, "Storage Unit", "Description", "Example Address", "Test Department")]
        public async Task Execute_Should_Get_StorageUnit_List(int count, string namePrefix, string descriptionPrefix, string addressPrefix, string departmentName)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();
            var useCase = scope.ServiceProvider.GetRequiredService<IRequestHandler<GetStorageUnitListQuery, IEnumerable<StorageUnitDto>>>();

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

            var storageUnits = Enumerable.Range(1, count).Select(i => new Domain.Entities.Locations.StorageUnit
            {
                Name = $"{namePrefix} {i}",
                Description = $"{descriptionPrefix} {i}",
                Address = $"{addressPrefix} {i}",
                DepartmentId = department.Id
            }).ToList();

            dbContext.StorageUnits.AddRange(storageUnits);
            dbContext.SaveChanges();

            var requested = await useCase.Handle(new GetStorageUnitListQuery(), CancellationToken.None);

            Assert.NotNull(requested);
            Assert.Equal(count, requested.Count());

            dbContext.StorageUnits.RemoveRange(storageUnits);
            dbContext.Remove(department);
            dbContext.SaveChanges();
        }
    }
}
