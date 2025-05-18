// using Hospital.Application;
// using Hospital.Application.UseCases.Patient.Commands;
// using Hospital.Domain.Enums;
// using Hospital.Infrastructure.Persistance;
// using Hospital.IntegrationTests.Configuration;
// using Microsoft.Extensions.DependencyInjection;

// namespace Hospital.IntegrationTests.Tests.Patient.Commands
// {
//     [Collection("ServiceProvider collection")]
//     public class DeletePatientUseCaseTests(TestServiceProviderFixture fixture)
//     {
//         private readonly IServiceProvider _serviceProvider = fixture.ServiceProvider;

//         [Theory]
//         [InlineData("John", "Doe", "john.doe", "password123", Gender.Male)]
//         public async Task Execute_Should_Delete_Patient(
//             string firstName, string lastName, string username, string password, Gender gender)
//         {
//             using var scope = _serviceProvider.CreateScope();
//             var dbContext = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();
//             var executor = scope.ServiceProvider.GetRequiredService<IUseCaseExecutor>();

//             var dateOfBirth = new DateTime(1990, 1, 1);
//             var generalPractitionerId = Guid.NewGuid();

//             var patient = new Domain.Entities.Users.PatientProfile
//             {
//                 FirstName = firstName,
//                 LastName = lastName,
//                 DateOfBirth = dateOfBirth,
//                 Gender = gender,
//                 Username = username,
//                 Password = password,
//                 GeneralPractitionerId = generalPractitionerId,
//                 MedicalHistoryId = Guid.Empty
//             };

//             dbContext.Patients.Add(patient);
//             dbContext.SaveChanges();

//             var deletedId = await executor.Dispatch(new DeletePatientCommand(patient.Id));

//             dbContext.SaveChanges();

//             // Assert.Equal(patient.Id, deletedId);
//             var deleted = dbContext.Patients.Find(patient.Id);
//             Assert.Null(deleted);
//         }

//         [Fact]
//         public async Task Execute_Should_Return_Empty_Guid_For_NonExistent_Id()
//         {
//             using var scope = _serviceProvider.CreateScope();
//             var executor = scope.ServiceProvider.GetRequiredService<IUseCaseExecutor>();

//             var nonExistentId = Guid.NewGuid();
//             var result = await executor.Dispatch(new DeletePatientCommand(nonExistentId));

//             Assert.Equal(Guid.Empty, result.Data);
//         }
//     }
// }
