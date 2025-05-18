using Hospital.Application.Interfaces;
using Hospital.Application.UseCases.Room.Commands;
using Hospital.Infrastructure.Persistance;
using Hospital.IntegrationTests.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.IntegrationTests.Tests.Room.Commands
{
    [Collection("ServiceProvider collection")]
    public class DeleteRoomUseCaseTests(TestServiceProviderFixture fixture)
    {
        private readonly IServiceProvider _serviceProvider = fixture.ServiceProvider;

        [Theory]
        [InlineData("Room A", "Description A", "123 Test St", 101, 1)]
        public async Task Execute_Should_Delete_Room(string name, string description, string address, int roomNumber, int floor)
        {
            var id = Guid.NewGuid();
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();
                var useCase = scope.ServiceProvider.GetRequiredService<IRequestHandler<DeleteRoomCommand, Guid>>();
                var room = dbContext.Rooms.Add(new Domain.Entities.Locations.Room
                {
                    Id = id,
                    Name = name,
                    Description = description,
                    Address = address,
                    RoomNumber = roomNumber,
                    Floor = floor,
                    DepartmentId = Guid.NewGuid()
                }).Entity;
                dbContext.SaveChanges();
            }

            // Act (in a new scope)
            using (var scope = _serviceProvider.CreateScope())
            {
                var useCase = scope.ServiceProvider.GetRequiredService<IRequestHandler<DeleteRoomCommand, Guid>>();
                var result = await useCase.Handle(new DeleteRoomCommand(id), CancellationToken.None);
                Assert.Equal(id, result);
            }

            // Assert (in a third scope to check DB state cleanly)
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();
                var deleted = await dbContext.Hospitals.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);
                Assert.Null(deleted);
            }
        }
    }
}
