using System.ComponentModel.DataAnnotations;
using Hospital.Application.Interfaces;

namespace Hospital.Application.UseCases.Prescription.Commands;
public record AddPrescriptionCommand(
    [Required] DateTime PrescriptionDate,
    [Required] Guid GeneralPractitionerCheckUpId) : IRequest<Guid>;
public class AddPrescriptionCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddPrescriptionCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Guid> Handle(AddPrescriptionCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Prescriptions.AddAsync(new Domain.Entities.MedicalHistory.Prescription
        {
            PrescriptionDate = request.PrescriptionDate,
            Status = Domain.Enums.PrescriptionStatus.Active,
            GeneralPractitionerCheckUpId = request.GeneralPractitionerCheckUpId,
            PrescriptionLines = []
        });

        await _unitOfWork.CompleteAsync();

        return result;
    }
}
