using System.ComponentModel.DataAnnotations;
using Hospital.Application.Interfaces;

namespace Hospital.Application.UseCases.MedicalHistory.Commands;
public record DeleteMedicalHistoryCommand([Required] Guid Id) : IRequest<Guid>;
public class DeleteMedicalHistoryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteMedicalHistoryCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Guid> Handle(DeleteMedicalHistoryCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.MedicalHistories.Remove(request.Id);
        await _unitOfWork.CompleteAsync();
        return result;
    }
}
