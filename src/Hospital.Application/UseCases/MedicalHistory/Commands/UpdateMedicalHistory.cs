using System.ComponentModel.DataAnnotations;
using Hospital.Application.Interfaces;

namespace Hospital.Application.UseCases.MedicalHistory.Commands;

public record UpdateMedicalHistoryCommand([Required] Guid Id) : IRequest<Guid>;
public class UpdateMedicalHistoryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateMedicalHistoryCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Guid> Handle(UpdateMedicalHistoryCommand request, CancellationToken cancellationToken)
    {
        var medicalHistory = await _unitOfWork.MedicalHistories.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException("Medical history not found.");

        await _unitOfWork.MedicalHistories.Update(medicalHistory);
        await _unitOfWork.CompleteAsync();
        return medicalHistory.Id;
    }
}
