using System.ComponentModel.DataAnnotations;
using Hospital.Application.Interfaces;

namespace Hospital.Application.UseCases.Patient.Commands;

public record AddPatientCommand(
    [Required] string FirstName,
    [Required] string LastName,
    [Required] DateTime DateOfBirth,
    [Required] int Gender,
    [Required] string Username,
    [Required] string Password,
    [Required] Guid GeneralPractitionerId) : IRequest<Guid>;
// public class AddPatientCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddPatientCommand, Guid>
// {
//     private readonly IUnitOfWork _unitOfWork = unitOfWork;
//     public async Task<Guid> Handle(AddPatientCommand request, CancellationToken cancellationToken)
//     {
//         var result = await _unitOfWork.Patients.AddAsync(new Domain.Entities.Users.PatientProfile
//         {
//             FirstName = request.FirstName,
//             LastName = request.LastName,
//             DateOfBirth = request.DateOfBirth,
//             Gender = (Domain.Enums.Gender)request.Gender,
//             Username = request.Username,
//             Password = request.Password,
//             GeneralPractitionerId = request.GeneralPractitionerId,
//             MedicalHistoryId = Guid.Empty
//         });

//         await _unitOfWork.CompleteAsync();

//         return result;
//     }
// }