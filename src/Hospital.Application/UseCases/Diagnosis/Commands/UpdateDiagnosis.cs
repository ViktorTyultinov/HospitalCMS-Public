using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Mapster;

namespace Hospital.Application.UseCases.Diagnosis.Commands;

public record UpdateDiagnosisCommand(Domain.Entities.MedicalHistory.Diagnosis Diagnosis) : IRequest<DiagnosisDto>;
public class UpdateDiagnosisCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateDiagnosisCommand, DiagnosisDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<DiagnosisDto> Handle(UpdateDiagnosisCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Diagnoses.Update(request.Diagnosis);
        await _unitOfWork.CompleteAsync();
        return result.Adapt<DiagnosisDto>();
    }
}