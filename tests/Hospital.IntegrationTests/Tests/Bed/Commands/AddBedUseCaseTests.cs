using Hospital.Application.Interfaces;
using Hospital.Application.UseCases.Bed.Commands;
using Hospital.Infrastructure.Persistance;
using Hospital.IntegrationTests.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.IntegrationTests.Tests.Bed.Commands
{
    [Collection("ServiceProvider collection")]
    public class AddBedUseCaseTests(TestServiceProviderFixture fixture)
    {
        private readonly IServiceProvider _serviceProvider = fixture.ServiceProvider;

        [Theory]
        [InlineData(101)]
        public async Task Execute_Should_Add_Bed(int bedNumber)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();
            var useCase = scope.ServiceProvider.GetRequiredService<IRequestHandler<AddBedCommand, Guid>>();

            var roomId = Guid.NewGuid();
            var added = await useCase.Handle(new AddBedCommand(bedNumber, roomId, Guid.NewGuid()), CancellationToken.None);

            dbContext.SaveChanges();

            var requested = dbContext.Beds.FirstOrDefault(b => b.Id == added);

            Assert.NotNull(requested);
            Assert.Equal(bedNumber, requested.BedNumber);
            Assert.Equal(roomId, requested.RoomId);

            dbContext.Remove(requested);
            dbContext.SaveChanges();
        }
    }
}
