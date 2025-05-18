using Hospital.Application.Interfaces;
using Hospital.Application.UseCases.Hospital.Commands;
using Hospital.Infrastructure.Persistance;
using Hospital.IntegrationTests.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.IntegrationTests.Tests.Hospital.Commands;

[Collection("ServiceProvider collection")]
public class AddHospitalUseCaseTests(TestServiceProviderFixture fixture)
{
    private readonly IServiceProvider _serviceProvider = fixture.ServiceProvider;

    [Theory]
    [InlineData("Test Hospital", "123 Test St")]
    [InlineData("Test Hospital 2", "124 Test St")]
    public async Task Execute_Should_Add_Hospital(string name, string address)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();
        var useCase = scope.ServiceProvider.GetRequiredService<IRequestHandler<AddHospitalCommand, Guid>>();

        var added = await useCase.Handle(new AddHospitalCommand(name, address), CancellationToken.None);

        dbContext.SaveChanges();

        var requested = dbContext.Hospitals.FirstOrDefault(h => h.Id == added);

        Assert.NotNull(requested);
        Assert.Equal(name, requested.Name);
        Assert.Equal(address, requested.Address);

        dbContext.Remove(requested);
        dbContext.SaveChanges();
    }
}