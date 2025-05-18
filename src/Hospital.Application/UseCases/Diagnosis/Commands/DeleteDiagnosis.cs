using System.ComponentModel.DataAnnotations;
using Hospital.Application.Interfaces;

namespace Hospital.Application.UseCases.Diagnosis.Commands;
public record DeleteDiagnosisCommand([Required] Guid Id) : IRequest<Guid>;
public class DeleteDiagnosisCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteDiagnosisCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Guid> Handle(DeleteDiagnosisCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Diagnoses.Remove(request.Id);
        await _unitOfWork.CompleteAsync();
        return result;
    }
}