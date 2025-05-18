using Hospital.Application;
using Hospital.Application.UseCases.Patient.Commands;
using Hospital.Domain.Enums;
using Hospital.Infrastructure.Persistance;
using Hospital.IntegrationTests.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.IntegrationTests.Tests.Patient.Commands
{
    [Collection("ServiceProvider collection")]
    public class AddPatientUseCaseTests(TestServiceProviderFixture fixture)
    {
        private readonly IServiceProvider _serviceProvider = fixture.ServiceProvider;

        // [Theory]
        // [InlineData("John", "Doe", "john.doe", "password123", Gender.Male)]
        // public async Task Execute_Should_Add_Patient(
        //     string firstName, string lastName, string username, string password, Gender gender)
        // {
        //     using var scope = _serviceProvider.CreateScope();
        //     var dbContext = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();
        //     var executor = scope.ServiceProvider.GetRequiredService<IUseCaseExecutor>();

        //     var dateOfBirth = new DateTime(1990, 1, 1);
        //     var generalPractitionerId = Guid.NewGuid();

        //     var patientId = await executor.Dispatch(new AddPatientCommand(
        //         firstName, lastName, dateOfBirth, (int)gender, username, password, generalPractitionerId));

        //     var patient = dbContext.Patients.Find(patientId);
        //     Assert.NotNull(patient);
        //     Assert.Equal(firstName, patient.FirstName);
        //     Assert.Equal(lastName, patient.LastName);
        //     Assert.Equal(dateOfBirth, patient.DateOfBirth);
        //     Assert.Equal(gender, patient.Gender);
        //     Assert.Equal(username, patient.Username);
        //     Assert.Equal(password, patient.Password);
        //     Assert.Equal(generalPractitionerId, patient.GeneralPractitionerId);
        //     Assert.Equal(Guid.Empty, patient.MedicalHistoryId);

        //     dbContext.Remove(patient);
        //     dbContext.SaveChanges();
        // }
    }
}
