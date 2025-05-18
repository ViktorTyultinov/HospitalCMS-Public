using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Hospital.Application.UseCases.Hospital.Commands;
using Hospital.Infrastructure.Persistance;
using Hospital.IntegrationTests.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.IntegrationTests.Tests.Hospital.Commands;

[Collection("ServiceProvider collection")]
public class UpdateHospitalUseCaseTests(TestServiceProviderFixture fixture)
{
    private readonly IServiceProvider _serviceProvider = fixture.ServiceProvider;

    [Theory]
    [InlineData("Test Hospital", "123 Test St")]
    [InlineData("Test Hospital 2", "124 Test St")]
    public async Task Execute_Should_Update_Hospital(string name, string address)
    {
        // Create a new scope for each test to ensure isolation
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();
        var useCase = scope.ServiceProvider.GetRequiredService<IRequestHandler<UpdateHospitalCommand, HospitalDto>>();

        var added = dbContext.Hospitals.Add(new Domain.Entities.Locations.Hospital
        {
            Name = "Old Name",
            Address = "Old Address",
        }).Entity;

        dbContext.SaveChanges();

        added.Name = name;
        added.Address = address;

        var requested = await useCase.Handle(new UpdateHospitalCommand(added), CancellationToken.None);

        Assert.Equal(name, requested.Name);
        Assert.Equal(address, requested.Address);

        dbContext.Remove(added);
        dbContext.SaveChanges();
    }
}