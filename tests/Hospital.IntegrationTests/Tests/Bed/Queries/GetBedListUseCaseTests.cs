using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Hospital.Application.UseCases.Bed.Queries;
using Hospital.Infrastructure.Persistance;
using Hospital.IntegrationTests.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.IntegrationTests.Tests.Bed.Queries
{
    [Collection("ServiceProvider collection")]
    public class GetBedListUseCaseTests(TestServiceProviderFixture fixture)
    {
        private readonly IServiceProvider _serviceProvider = fixture.ServiceProvider;

        [Theory]
        [InlineData(3)]
        public async Task Execute_Should_Get_Bed_List(int bedCount)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();
            var useCase = scope.ServiceProvider.GetRequiredService<IRequestHandler<GetBedListQuery, IEnumerable<BedDto>>>();

            var beds = Enumerable.Range(1, bedCount).Select(i => new Domain.Entities.Locations.Bed
            {
                BedNumber = i,
                RoomId = Guid.NewGuid()
            }).ToList();

            dbContext.Beds.AddRange(beds);
            dbContext.SaveChanges();

            var requested = await useCase.Handle(new GetBedListQuery(), CancellationToken.None);

            Assert.NotNull(requested);
            Assert.Equal(bedCount, requested.Count());

            dbContext.Beds.RemoveRange(beds);
            dbContext.SaveChanges();
        }
    }
}
