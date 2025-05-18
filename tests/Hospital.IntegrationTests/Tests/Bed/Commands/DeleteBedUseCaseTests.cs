using Hospital.Application.Interfaces;
using Hospital.Application.UseCases.Bed.Commands;
using Hospital.Infrastructure.Persistance;
using Hospital.IntegrationTests.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.IntegrationTests.Tests.Bed.Commands
{
    [Collection("ServiceProvider collection")]
    public class DeleteBedUseCaseTests(TestServiceProviderFixture fixture)
    {
        private readonly IServiceProvider _serviceProvider = fixture.ServiceProvider;

        [Theory]
        [InlineData(101)]
        public async Task Execute_Should_Delete_Bed(int bedNumber)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();
            var useCase = scope.ServiceProvider.GetRequiredService<IRequestHandler<DeleteBedCommand, Guid>>();

            var bed = dbContext.Beds.Add(new Domain.Entities.Locations.Bed
            {
                BedNumber = bedNumber,
                RoomId = Guid.NewGuid(),
            }).Entity;
            dbContext.SaveChanges();

            var deleted = await useCase.Handle(new DeleteBedCommand(bed.Id), CancellationToken.None);
            dbContext.SaveChanges();

            var requested = dbContext.Beds.FirstOrDefault(b => b.Id == deleted);

            Assert.Null(requested);
            dbContext.SaveChanges();
        }
    }
}
