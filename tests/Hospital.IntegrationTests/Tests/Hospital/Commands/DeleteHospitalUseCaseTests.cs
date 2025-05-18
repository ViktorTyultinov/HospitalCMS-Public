using Hospital.Application.Interfaces;
using Hospital.Application.UseCases.Hospital.Commands;
using Hospital.Infrastructure.Persistance;
using Hospital.IntegrationTests.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.IntegrationTests.Tests.Hospital.Commands;

[Collection("ServiceProvider collection")]
public class DeleteHospitalUseCaseTests(TestServiceProviderFixture fixture)
{
    private readonly IServiceProvider _serviceProvider = fixture.ServiceProvider;

    [Theory]
    [InlineData("Test Hospital", "123 Test St")]
    [InlineData("Test Hospital 2", "124 Test St")]
    public async Task Execute_Should_Delete_Hospital(string name, string address)
    {
        // Arrange
        var id = Guid.NewGuid();

        // Create scope A for seeding
        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();
            dbContext.Hospitals.Add(new Domain.Entities.Locations.Hospital
            {
                Id = id,
                Name = name,
                Address = address,
            });
            await dbContext.SaveChangesAsync();
        }

        // Act (in a new scope)
        using (var scope = _serviceProvider.CreateScope())
        {
            var useCase = scope.ServiceProvider.GetRequiredService<IRequestHandler<DeleteHospitalCommand, Guid>>();
            var result = await useCase.Handle(new DeleteHospitalCommand(id), CancellationToken.None);
            Assert.Equal(id, result);
        }

        // Assert (in a third scope to check DB state cleanly)
        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();
            var deleted = await dbContext.Hospitals.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);
            Assert.Null(deleted);
        }
    }
}
