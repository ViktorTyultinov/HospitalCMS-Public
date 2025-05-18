using Hospital.Application.UseCases.Hospital.Queries;
using Hospital.Application.Interfaces;
using Moq;
using Hospital.Domain.Interfaces.Repositories;

namespace Hospital.UnitTests.Tests.Hospital.Queries;
public class GetHospitalByIdQueryHandlerTests
{
    [Fact]
    public async Task Execute_Should_GetHospitalById()
    {
        // Arrange
        var hospitalId = Guid.NewGuid();
        var expectedHospital = new Domain.Entities.Locations.Hospital
        {
            Id = hospitalId,
            Name = "Test Hospital",
            Address = "123 Test St"
        };

        // Mocking the IHospitalRepository and IUnitOfWork
        var hospitalRepositoryMock = new Mock<IHospitalRepository>();
        hospitalRepositoryMock.Setup(repo => repo.GetByIdAsync(hospitalId))
            .ReturnsAsync(expectedHospital);  // Return the expected hospital entity

        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(uow => uow.Hospitals)
            .Returns(hospitalRepositoryMock.Object); // Mock the unit of work to return our mocked repository

        // Instantiate the query handler with the mocked unit of work
        var queryHandler = new GetHospitalByIdQueryHandler(unitOfWorkMock.Object);

        // Act
        var result = await queryHandler.Handle(new GetHospitalByIdQuery(hospitalId), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedHospital.Id, result.Id);
        Assert.Equal(expectedHospital.Name, result.Name);
        Assert.Equal(expectedHospital.Address, result.Address);

        // Verify that the repository method was called with the correct Id
        hospitalRepositoryMock.Verify(repo => repo.GetByIdAsync(hospitalId), Times.Once);
        unitOfWorkMock.Verify(uow => uow.Hospitals, Times.Once);
    }
}
