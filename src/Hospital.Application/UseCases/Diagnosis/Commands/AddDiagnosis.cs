using System.ComponentModel.DataAnnotations;
using Hospital.Application.Interfaces;

namespace Hospital.Application.UseCases.Diagnosis.Commands;
public record AddDiagnosisCommand(
    [Required] string Name,
    [Required] string Description,
    [Required] Guid GeneralPractitionerId) : IRequest<Guid>;

public class AddDiagnosisCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddDiagnosisCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Guid> Handle(AddDiagnosisCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Diagnoses.AddAsync(new Domain.Entities.MedicalHistory.Diagnosis
        {
            DiagnosisName = request.Name,
            Description = request.Description,
            GeneralPractitionerCheckUpId = request.GeneralPractitionerId,
        });

        await _unitOfWork.CompleteAsync();

        return result;
    }
}