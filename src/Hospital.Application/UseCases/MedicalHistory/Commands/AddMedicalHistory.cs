using System.ComponentModel.DataAnnotations;
using Hospital.Application.Interfaces;

namespace Hospital.Application.UseCases.MedicalHistory.Commands;
public record AddMedicalHistoryCommand([Required] Guid PatientId) : IRequest<Guid>;
public class AddMedicalHistoryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddMedicalHistoryCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Guid> Handle(AddMedicalHistoryCommand request, CancellationToken cancellationToken)
    {

        var medicalHistory = new Domain.Entities.MedicalHistory.PatientMedicalHistory
        {
            Id = Guid.NewGuid()
        };

        var result = await _unitOfWork.MedicalHistories.AddAsync(medicalHistory);

        await _unitOfWork.CompleteAsync();

        return result;
    }
}