using System.ComponentModel.DataAnnotations;
using Hospital.Application.Interfaces;

namespace Hospital.Application.UseCases.Specialist.Commands;

public record AddSpecialistCommand(
    [Required] string FirstName,
    [Required] string LastName,
    [Required] DateTime DateOfBirth,
    [Required] int Gender,
    [Required] string Username,
    [Required] string Password,
    [Required] int Specialty,
    [Required] Guid HospitalId) : IRequest<Guid>;
public class AddSpecialistCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddSpecialistCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Guid> Handle(AddSpecialistCommand request, CancellationToken cancellationToken)
    {
        // var result = await _unitOfWork.Specialists.AddAsync(new Domain.Entities.Users.SpecialistProfile
        // {
        //     FirstName = request.FirstName,
        //     LastName = request.LastName,
        //     DateOfBirth = request.DateOfBirth,
        //     Gender = (Domain.Enums.Gender)request.Gender,
        //     Username = request.Username,
        //     Password = request.Password,
        //     Specialty = (Domain.Enums.Specialty)request.Specialty,
        //     HospitalId = request.HospitalId,
        // });

        await _unitOfWork.CompleteAsync();

        return Guid.NewGuid();
    }
}