using Hospital.Application.Interfaces;
using Hospital.Application.UseCases.Room.Commands;
using Hospital.Infrastructure.Persistance;
using Hospital.IntegrationTests.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.IntegrationTests.Tests.Room.Commands
{
    [Collection("ServiceProvider collection")]
    public class AddRoomUseCaseTests(TestServiceProviderFixture fixture)
    {
        private readonly IServiceProvider _serviceProvider = fixture.ServiceProvider;

        [Theory]
        [InlineData("Room A", "Description A", "123 Test St", 101, 1)]
        public async Task Execute_Should_Add_Room(string name, string description, string address, int roomNumber, int floor)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();
            var useCase = scope.ServiceProvider.GetRequiredService<IRequestHandler<AddRoomCommand, Guid>>();

            var department = dbContext.Departments.Add(new Domain.Entities.Locations.Department
            {
                Name = "Test Department",
                Description = "Test Description",
                Address = "123 Test St",
                HospitalId = Guid.NewGuid(),
                Rooms = [],
                StorageUnits = []
            }).Entity;
            
            dbContext.SaveChanges();

            var added = await useCase.Handle(new AddRoomCommand(name, description, address, roomNumber, floor, department.Id), CancellationToken.None);

            dbContext.SaveChanges();

            var requested = dbContext.Rooms.FirstOrDefault(r => r.Id == added);

            Assert.NotNull(requested);
            Assert.Equal(name, requested.Name);
            Assert.Equal(description, requested.Description);
            Assert.Equal(address, requested.Address);
            Assert.Equal(roomNumber, requested.RoomNumber);
            Assert.Equal(floor, requested.Floor);

            dbContext.Remove(requested);
            dbContext.Remove(department);
            dbContext.SaveChanges();
        }
    }
}
