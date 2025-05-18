using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Mapster;

namespace Hospital.Application.UseCases.Patient.Commands;

public record UpdatePatientCommand(Domain.Entities.Users.PatientProfile Patient) : IRequest<PatientDto>;
public class UpdatePatientCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdatePatientCommand, PatientDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<PatientDto> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Patients.Update(request.Patient);
        await _unitOfWork.CompleteAsync();
        return result.Adapt<PatientDto>();
    }
}