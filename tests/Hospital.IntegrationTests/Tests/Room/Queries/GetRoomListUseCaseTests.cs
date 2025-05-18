using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Hospital.Application.UseCases.Room.Queries;
using Hospital.Infrastructure.Persistance;
using Hospital.IntegrationTests.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.IntegrationTests.Tests.Room.Queries
{
    [Collection("ServiceProvider collection")]
    public class GetRoomListUseCaseTests(TestServiceProviderFixture fixture)
    {
        private readonly IServiceProvider _serviceProvider = fixture.ServiceProvider;

        [Theory]
        [InlineData(3, "Room", "Description", "123 Test St")]
        public async Task Execute_Should_Get_Room_List(int roomCount, string namePrefix, string descriptionPrefix, string addressPrefix)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();
            var useCase = scope.ServiceProvider.GetRequiredService<IRequestHandler<GetRoomListQuery, IEnumerable<RoomDto>>>();

            var department = dbContext.Departments.Add(new Domain.Entities.Locations.Department
            {
                Name = "Test Department",
                Description = "Test Description",
                Address = "123 Test St",
                HospitalId = Guid.NewGuid(),
                Rooms = [],
                StorageUnits = []
            }).Entity;

            var rooms = Enumerable.Range(1, roomCount).Select(i => new Domain.Entities.Locations.Room
            {
                Name = $"{namePrefix} {i}",
                Description = $"{descriptionPrefix} {i}",
                Address = $"{addressPrefix} {i}",
                RoomNumber = 100 + i,
                Floor = i,
                DepartmentId = department.Id
            }).ToList();

            dbContext.Rooms.AddRange(rooms);
            dbContext.SaveChanges();

            var requested = await useCase.Handle(new GetRoomListQuery(), CancellationToken.None);

            Assert.NotNull(requested);
            Assert.Equal(roomCount, requested.Count());

            dbContext.Rooms.RemoveRange(rooms);
            dbContext.Remove(department);
            dbContext.SaveChanges();
        }
    }
}
