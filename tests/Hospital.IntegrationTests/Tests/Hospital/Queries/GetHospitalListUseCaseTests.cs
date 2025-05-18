using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Hospital.Application.UseCases.Hospital.Queries;
using Hospital.Infrastructure.Persistance;
using Hospital.IntegrationTests.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.IntegrationTests.Tests.Hospital.Queries;

[Collection("ServiceProvider collection")]
public class GetHospitalListUseCaseTests(TestServiceProviderFixture fixture)
{
    private readonly IServiceProvider _serviceProvider = fixture.ServiceProvider;

    [Theory]
    [InlineData("Test Hospital", "123 Test St", 5)]
    public async Task Execute_Should_Get_Hospital_List(string name, string address, int hospitalCount)
    {
        // Create a new scope for each test to ensure isolation
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();
        var useCase = scope.ServiceProvider.GetRequiredService<IRequestHandler<GetHospitalListQuery, IEnumerable<HospitalDto>>>();

        var dummy = new List<Domain.Entities.Locations.Hospital>();
        foreach (var i in Enumerable.Range(1, hospitalCount))
        {
            var hospital = new Domain.Entities.Locations.Hospital
            {
                Name = $"{name} {i}",
                Address = $"{address} {i}",
            };

            dummy.Add(hospital);
        }

        dbContext.Hospitals.AddRange(dummy);
        dbContext.SaveChanges();

        IEnumerable<HospitalDto> hospitals = await useCase.Handle(new GetHospitalListQuery(), CancellationToken.None);

        Assert.NotNull(hospitals);
        Assert.Equal(hospitalCount, hospitals.Count());
        Assert.Equal(dummy.First().Name, hospitals.First().Name);
        Assert.Equal(dummy.First().Address, hospitals.First().Address);

        dbContext.Hospitals.RemoveRange(dummy);
        dbContext.SaveChanges();
    }
}