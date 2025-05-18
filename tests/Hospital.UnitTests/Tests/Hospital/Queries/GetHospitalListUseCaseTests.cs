
using Hospital.Application.UseCases.Hospital.Queries;
using Hospital.Application.Interfaces;
using Moq;
using Hospital.Domain.Interfaces.Repositories;

namespace Hospital.UnitTests.Tests.Hospital.Queries;

public class GetHospitalListQueryHandlerTests
{
    [Fact]
    public async Task Execute_Should_GetHospitalList()
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
        hospitalRepositoryMock.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync([expectedHospital]);  // Return the expected hospital entity

        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(uow => uow.Hospitals)
            .Returns(hospitalRepositoryMock.Object); // Mock the unit of work to return our mocked repository

        // Instantiate the query handler with the mocked unit of work
        var queryHandler = new GetHospitalListQueryHandler(unitOfWorkMock.Object);

        // Act
        var result = await queryHandler.Handle(new GetHospitalListQuery(), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedHospital.Id, result.First().Id);
        Assert.Equal(expectedHospital.Name, result.First().Name);
        Assert.Equal(expectedHospital.Address, result.First().Address);

        // Verify that the repository method was called with the correct Id
        hospitalRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
    }
}
