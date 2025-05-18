using System.ComponentModel.DataAnnotations;
using Hospital.Application.Interfaces;

namespace Hospital.Application.UseCases.PrescriptionLine.Commands;

public record AddPrescriptionLineCommand(
    [Required] string MedicationName,
    [Required] string Dosage,
    [Required] string Frequency,
    string? Instructions,
    [Required] int Duration,
    [Required] Guid PrescriptionId) : IRequest<Guid>;
public class AddPrescriptionLineCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddPrescriptionLineCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Guid> Handle(AddPrescriptionLineCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.PrescriptionLines.AddAsync(new Domain.Entities.MedicalHistory.PrescriptionLine
        {
            MedicationName = request.MedicationName,
            Dosage = request.Dosage,
            Frequency = request.Frequency,
            Instructions = request.Instructions,
            Duration = request.Duration,
            PrescriptionId = request.PrescriptionId,
        });

        await _unitOfWork.CompleteAsync();

        return result;
    }
}