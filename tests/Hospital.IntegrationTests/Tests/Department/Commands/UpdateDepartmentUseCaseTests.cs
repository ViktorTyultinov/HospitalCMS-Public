using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Hospital.Application.UseCases.Department.Commands;
using Hospital.Infrastructure.Persistance;
using Hospital.IntegrationTests.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.IntegrationTests.Tests.Department.Commands
{
    [Collection("ServiceProvider collection")]
    public class UpdateDepartmentUseCaseTests(TestServiceProviderFixture fixture)
    {
        private readonly IServiceProvider _serviceProvider = fixture.ServiceProvider;

        [Theory]
        [InlineData("Updated Department", "Updated Description", "456 Updated St")]
        public async Task Execute_Should_Update_Department(string name, string description, string address)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();
            var useCase = scope.ServiceProvider.GetRequiredService<IRequestHandler<UpdateDepartmentCommand, DepartmentDto>>();

            var department = dbContext.Departments.Add(new Domain.Entities.Locations.Department
            {
                Name = "Old Department",
                Description = "Old Description",
                Address = "123 Old St",
                HospitalId = Guid.NewGuid(),
                StorageUnits = [],
                Rooms = [],
            }).Entity;

            dbContext.SaveChanges();

            department.Name = name;
            department.Description = description;
            department.Address = address;

            var updated = await useCase.Handle(new UpdateDepartmentCommand(department), CancellationToken.None);

            Assert.Equal(name, updated.Name);
            Assert.Equal(description, updated.Description);
            Assert.Equal(address, updated.Address);

            dbContext.Remove(department);
            dbContext.SaveChanges();
        }
    }
}
