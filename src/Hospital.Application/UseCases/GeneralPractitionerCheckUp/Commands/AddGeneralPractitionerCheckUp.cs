using System.ComponentModel.DataAnnotations;
using Hospital.Application.Interfaces;

namespace Hospital.Application.UseCases.GeneralPractitionerCheckUp.Commands;

public record AddGeneralPractitionerCheckUpCommand(
    [Required] DateTime CheckupDate,
    string? Notes,
    [Required] Guid DiagnosisId,
    [Required] Guid PrescriptionId,
    [Required] Guid PatientId,
    [Required] Guid GeneralPractitionerId) : IRequest<Guid>;

public class AddGeneralPractitionerCheckUpCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddGeneralPractitionerCheckUpCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Guid> Handle(AddGeneralPractitionerCheckUpCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.GeneralPractitionerCheckUps.AddAsync(new Domain.Entities.MedicalHistory.GeneralPractitionerCheckUp
        {
            CheckupDate = request.CheckupDate,
            Notes = request.Notes,
            DiagnosisId = request.DiagnosisId,
            PrescriptionId = request.PrescriptionId,
            PatientId = request.PatientId,
            GeneralPractitionerId = request.GeneralPractitionerId,
            CreatedAt = DateTime.UtcNow
        });

        await _unitOfWork.CompleteAsync();

        return result;
    }
}