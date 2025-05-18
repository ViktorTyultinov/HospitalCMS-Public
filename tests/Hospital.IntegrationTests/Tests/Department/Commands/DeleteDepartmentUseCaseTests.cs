using Hospital.Application.Interfaces;
using Hospital.Application.UseCases.Department.Commands;
using Hospital.Infrastructure.Persistance;
using Hospital.IntegrationTests.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.IntegrationTests.Tests.Department.Commands
{
    [Collection("ServiceProvider collection")]
    public class DeleteDepartmentUseCaseTests(TestServiceProviderFixture fixture)
    {
        private readonly IServiceProvider _serviceProvider = fixture.ServiceProvider;

        [Theory]
        [InlineData("Test Department", "Description", "123 Test St")]
        public async Task Execute_Should_Delete_Department(string name, string description, string address)
        {
            var id = Guid.NewGuid();

            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();

                dbContext.Departments.Add(new Domain.Entities.Locations.Department
                {
                    Id = id,
                    Name = name,
                    Description = description,
                    Address = address,
                    HospitalId = Guid.NewGuid(),
                    StorageUnits = [],
                    Rooms = [],
                });
                dbContext.SaveChanges();
            }

            using (var scope = _serviceProvider.CreateScope())
            {
                var useCase = scope.ServiceProvider.GetRequiredService<IRequestHandler<DeleteDepartmentCommand, Guid>>();
                var result = await useCase.Handle(new DeleteDepartmentCommand(id), CancellationToken.None);
                Assert.Equal(id, result);
            }

            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();
                var deleted = await dbContext.Departments.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
                Assert.Null(deleted);
            }
        }
    }
}
