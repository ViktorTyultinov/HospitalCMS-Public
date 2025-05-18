using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Hospital.Application.UseCases.Room.Queries;
using Hospital.Infrastructure.Persistance;
using Hospital.IntegrationTests.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.IntegrationTests.Tests.Room.Queries
{
    [Collection("ServiceProvider collection")]
    public class GetRoomByIdUseCaseTests(TestServiceProviderFixture fixture)
    {
        private readonly IServiceProvider _serviceProvider = fixture.ServiceProvider;

        [Theory]
        [InlineData("Room A", "Description A", "123 Test St", 101, 1)]
        public async Task Execute_Should_Get_Room_By_Id(string name, string description, string address, int roomNumber, int floor)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();
            var useCase = scope.ServiceProvider.GetRequiredService<IRequestHandler<GetRoomByIdQuery, RoomDto>>();

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
                Name = name,
                Description = description,
                Address = address,
                RoomNumber = roomNumber,
                Floor = floor,
                DepartmentId = department.Id
            }).Entity;
            dbContext.SaveChanges();

            var requested = await useCase.Handle(new GetRoomByIdQuery(room.Id), CancellationToken.None);

            Assert.NotNull(requested);
            Assert.Equal(name, requested.Name);
            Assert.Equal(description, requested.Description);
            Assert.Equal(address, requested.Address);
            Assert.Equal(roomNumber, requested.RoomNumber);
            Assert.Equal(floor, requested.Floor);

            dbContext.Remove(room);
            dbContext.Remove(department);
            dbContext.SaveChanges();
        }
    }
}
