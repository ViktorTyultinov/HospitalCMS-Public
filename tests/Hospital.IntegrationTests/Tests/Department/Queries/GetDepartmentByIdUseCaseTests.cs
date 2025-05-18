using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Hospital.Application.UseCases.Department.Queries;
using Hospital.Infrastructure.Persistance;
using Hospital.IntegrationTests.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.IntegrationTests.Tests.Department.Queries
{
    [Collection("ServiceProvider collection")]
    public class GetDepartmentByIdUseCaseTests(TestServiceProviderFixture fixture)
    {
        private readonly IServiceProvider _serviceProvider = fixture.ServiceProvider;

        [Theory]
        [InlineData("Test Department", "Description", "123 Test St")]
        public async Task Execute_Should_Get_Department_By_Id(string name, string description, string address)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();
            var useCase = scope.ServiceProvider.GetRequiredService<IRequestHandler<GetDepartmentByIdQuery, DepartmentDto>>();

            var department = dbContext.Departments.Add(new Domain.Entities.Locations.Department
            {
                Name = name,
                Description = description,
                Address = address,
                HospitalId = Guid.NewGuid(),
                StorageUnits = [],
                Rooms = [],
            }).Entity;

            dbContext.SaveChanges();

            var requested = await useCase.Handle(new GetDepartmentByIdQuery(department.Id), CancellationToken.None);

            Assert.NotNull(requested);
            Assert.Equal(name, requested.Name);
            Assert.Equal(description, requested.Description);
            Assert.Equal(address, requested.Address);

            dbContext.Remove(department);
            dbContext.SaveChanges();
        }
    }
}
