using Hospital.Application;
using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Hospital.Application.UseCases.Hospital.Queries;
using Hospital.Infrastructure.Persistance;
using Hospital.IntegrationTests.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.IntegrationTests.Tests.Hospital.Queries;

[Collection("ServiceProvider collection")]
public class GetHospitalByIdUseCaseTests(TestServiceProviderFixture fixture)
{
    private readonly IServiceProvider _serviceProvider = fixture.ServiceProvider;

    [Theory]
    [InlineData("Test Hospital", "123 Test St")]
    [InlineData("Test Hospital 2", "124 Test St")]
    public async Task Execute_Should_Get_Hospital_By_Id(string name, string address)
    {
        // Create a new scope for each test to ensure isolation
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();
        var useCase = scope.ServiceProvider.GetRequiredService<IRequestHandler<GetHospitalByIdQuery, HospitalDto>>();
        var executor = scope.ServiceProvider.GetRequiredService<IUseCaseExecutor>();

        var added = dbContext.Hospitals.Add(new Domain.Entities.Locations.Hospital
        {
            Name = name,
            Address = address,
        }).Entity;

        dbContext.SaveChanges();

        var requested = await executor.Dispatch(new GetHospitalByIdQuery(added.Id), null, CancellationToken.None);

        Assert.NotNull(requested);
        Assert.NotNull(requested.Data);
        Assert.Equal(name, requested.Data.Name);
        Assert.Equal(address, requested.Data.Address);

        dbContext.Remove(added);
        dbContext.SaveChanges();
    }

    // private static void SeedDatabase(HospitalDbContext dbContext)
    // {
    //     // Seeder function if it is needed.
    // }
}