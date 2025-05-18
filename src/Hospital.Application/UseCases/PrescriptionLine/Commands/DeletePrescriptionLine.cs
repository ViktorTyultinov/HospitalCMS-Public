using System.ComponentModel.DataAnnotations;
using Hospital.Application.Interfaces;

namespace Hospital.Application.UseCases.PrescriptionLine.Commands;

public record DeletePrescriptionLineCommand([Required] Guid Id) : IRequest<Guid>;
public class DeletePrescriptionLineCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeletePrescriptionLineCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Guid> Handle(DeletePrescriptionLineCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.PrescriptionLines.Remove(request.Id);
        await _unitOfWork.CompleteAsync();
        return result;
    }
}