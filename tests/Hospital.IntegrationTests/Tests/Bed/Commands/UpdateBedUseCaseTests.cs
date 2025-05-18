using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Hospital.Application.UseCases.Bed.Commands;
using Hospital.Infrastructure.Persistance;
using Hospital.IntegrationTests.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.IntegrationTests.Tests.Bed.Commands
{
    [Collection("ServiceProvider collection")]
    public class UpdateBedUseCaseTests(TestServiceProviderFixture fixture)
    {
        private readonly IServiceProvider _serviceProvider = fixture.ServiceProvider;

        [Theory]
        [InlineData(101, 102)]
        public async Task Execute_Should_Update_Bed(int oldBedNumber, int newBedNumber)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();
            var useCase = scope.ServiceProvider.GetRequiredService<IRequestHandler<UpdateBedCommand, BedDto>>();

            var bed = dbContext.Beds.Add(new Domain.Entities.Locations.Bed
            {
                BedNumber = oldBedNumber,
                RoomId = Guid.NewGuid(),
            }).Entity;
            dbContext.SaveChanges();

            bed.BedNumber = newBedNumber;
            var updated = await useCase.Handle(new UpdateBedCommand(bed), CancellationToken.None);

            dbContext.SaveChanges();

            var requested = dbContext.Beds.FirstOrDefault(b => b.Id == updated.Id);

            Assert.NotNull(requested);
            Assert.Equal(newBedNumber, requested.BedNumber);

            dbContext.Remove(requested);
            dbContext.SaveChanges();
        }
    }
}
