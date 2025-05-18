using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Hospital.Application.UseCases.Department.Queries;
using Hospital.Infrastructure.Persistance;
using Hospital.IntegrationTests.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.IntegrationTests.Tests.Department.Queries
{
    [Collection("ServiceProvider collection")]
    public class GetDepartmentListUseCaseTests(TestServiceProviderFixture fixture)
    {
        private readonly IServiceProvider _serviceProvider = fixture.ServiceProvider;

        [Theory]
        [InlineData("Test Department", "Description", "123 Test St", 3)]
        public async Task Execute_Should_Get_Department_List(string name, string description, string address, int count)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();
            var useCase = scope.ServiceProvider.GetRequiredService<IRequestHandler<GetDepartmentListQuery, IEnumerable<DepartmentDto>>>();

            var departments = Enumerable.Range(1, count).Select(i => new Domain.Entities.Locations.Department
            {
                Name = $"{name} {i}",
                Description = $"{description} {i}",
                Address = $"{address} {i}",
                HospitalId = Guid.NewGuid(),
                StorageUnits = [],
                Rooms = [],
            }).ToList();

            dbContext.Departments.AddRange(departments);
            dbContext.SaveChanges();

            var requested = await useCase.Handle(new GetDepartmentListQuery(), CancellationToken.None);

            Assert.NotNull(requested);
            Assert.Equal(count, requested.Count());

            dbContext.Departments.RemoveRange(departments);
            dbContext.SaveChanges();
        }
    }
}
