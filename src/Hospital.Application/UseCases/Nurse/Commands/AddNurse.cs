using System.ComponentModel.DataAnnotations;
using Hospital.Application.Interfaces;

namespace Hospital.Application.UseCases.Nurse.Commands;

public record AddNurseCommand(
    [Required] string FirstName,
    [Required] string LastName,
    [Required] DateTime DateOfBirth,
    [Required] int Gender,
    [Required] string Username,
    [Required] string Password,
    [Required] Guid HospitalId) : IRequest<Guid>;

public class AddNurseCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddNurseCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Guid> Handle(AddNurseCommand request, CancellationToken cancellationToken)
    {
        // var result = await _unitOfWork.Nurses.AddAsync(new Domain.Entities.Users.NurseProfile
        // {
        //     FirstName = request.FirstName,
        //     LastName = request.LastName,
        //     DateOfBirth = request.DateOfBirth,
        //     Gender = (Domain.Enums.Gender)request.Gender,
        //     Username = request.Username,
        //     Password = request.Password,
        //     HospitalId = request.HospitalId,
        // });

        await _unitOfWork.CompleteAsync();

        return Guid.NewGuid();
    }
}