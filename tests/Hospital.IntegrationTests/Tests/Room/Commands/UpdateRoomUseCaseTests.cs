using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Hospital.Application.UseCases.Room.Commands;
using Hospital.Infrastructure.Persistance;
using Hospital.IntegrationTests.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.IntegrationTests.Tests.Room.Commands
{
    [Collection("ServiceProvider collection")]
    public class UpdateRoomUseCaseTests(TestServiceProviderFixture fixture)
    {
        private readonly IServiceProvider _serviceProvider = fixture.ServiceProvider;

        [Theory]
        [InlineData("Room A", "Updated Room A", "Updated Description", "456 Updated St", 102, 2)]
        public async Task Execute_Should_Update_Room(string oldName, string newName, string newDescription, string newAddress, int newRoomNumber, int newFloor)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();
            var useCase = scope.ServiceProvider.GetRequiredService<IRequestHandler<UpdateRoomCommand, RoomDto>>();

            var department = dbContext.Departments.Add(new Domain.Entities.Locations.Department
            {
                Name = "Test Department",
                Description = "Test Description",
                Address = "123 Test St",
                HospitalId = Guid.NewGuid(),
                Rooms = [],
                StorageUnits = []
            }).Entity;

            var room = dbContext.Rooms.Add(new Domain.Entities.Locations.Room
            {
                Name = oldName,
                Description = "Old Description",
                Address = "123 Old St",
                RoomNumber = 101,
                Floor = 1,
                DepartmentId = department.Id
            }).Entity;
            dbContext.SaveChanges();

            room.Name = newName;
            room.Description = newDescription;
            room.Address = newAddress;
            room.RoomNumber = newRoomNumber;
            room.Floor = newFloor;

            var updated = await useCase.Handle(new UpdateRoomCommand(room), CancellationToken.None);

            dbContext.SaveChanges();

            var requested = dbContext.Rooms.FirstOrDefault(r => r.Id == updated.Id);

            Assert.NotNull(requested);
            Assert.Equal(newName, requested.Name);
            Assert.Equal(newDescription, requested.Description);
            Assert.Equal(newAddress, requested.Address);
            Assert.Equal(newRoomNumber, requested.RoomNumber);
            Assert.Equal(newFloor, requested.Floor);

            dbContext.Remove(requested);
            dbContext.Remove(department);
            dbContext.SaveChanges();
        }
    }
}
