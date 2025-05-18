using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Hospital.Application.UseCases.Bed.Queries;
using Hospital.Infrastructure.Persistance;
using Hospital.IntegrationTests.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.IntegrationTests.Tests.Bed.Queries
{
    [Collection("ServiceProvider collection")]
    public class GetBedByIdUseCaseTests(TestServiceProviderFixture fixture)
    {
        private readonly IServiceProvider _serviceProvider = fixture.ServiceProvider;

        [Theory]
        [InlineData(101)]
        public async Task Execute_Should_Get_Bed_By_Id(int bedNumber)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();
            var useCase = scope.ServiceProvider.GetRequiredService<IRequestHandler<GetBedByIdQuery, BedDto>>();

            var bed = dbContext.Beds.Add(new Domain.Entities.Locations.Bed
            {
                BedNumber = bedNumber,
                RoomId = Guid.NewGuid()
            }).Entity;
            dbContext.SaveChanges();

            var requested = await useCase.Handle(new GetBedByIdQuery(bed.Id), CancellationToken.None);

            Assert.NotNull(requested);
            Assert.Equal(bedNumber, requested.BedNumber);

            dbContext.Remove(bed);
            dbContext.SaveChanges();
        }
    }
}
