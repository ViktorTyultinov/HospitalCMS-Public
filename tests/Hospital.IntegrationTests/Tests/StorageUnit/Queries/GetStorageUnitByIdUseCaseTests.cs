using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Hospital.Application.UseCases.StorageUnit.Queries;
using Hospital.Infrastructure.Persistance;
using Hospital.IntegrationTests.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.IntegrationTests.Tests.StorageUnit.Queries
{
    [Collection("ServiceProvider collection")]
    public class GetStorageUnitByIdUseCaseTests(TestServiceProviderFixture fixture)
    {
        private readonly IServiceProvider _serviceProvider = fixture.ServiceProvider;

        [Theory]
        [InlineData("Storage Unit A", "Description A", "Example Address")]
        public async Task Execute_Should_Get_StorageUnit_By_Id(string name, string description, string address)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();
            var useCase = scope.ServiceProvider.GetRequiredService<IRequestHandler<GetStorageUnitByIdQuery, StorageUnitDto>>();

            var storageUnit = dbContext.StorageUnits.Add(new Domain.Entities.Locations.StorageUnit
            {
                Name = name,
                Description = description,
                Address = address,
                DepartmentId = Guid.NewGuid()
            }).Entity;
            dbContext.SaveChanges();

            var requested = await useCase.Handle(new GetStorageUnitByIdQuery(storageUnit.Id), CancellationToken.None);

            Assert.NotNull(requested);
            Assert.Equal(name, requested.Name);
            Assert.Equal(description, requested.Description);

            dbContext.Remove(storageUnit);
            dbContext.SaveChanges();
        }
    }
}
