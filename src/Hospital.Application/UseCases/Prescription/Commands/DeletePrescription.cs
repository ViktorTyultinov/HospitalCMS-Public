using System.ComponentModel.DataAnnotations;
using Hospital.Application.Interfaces;

namespace Hospital.Application.UseCases.Prescription.Commands;

public record DeletePrescriptionCommand([Required] Guid Id) : IRequest<Guid>;
public class DeletePrescriptionCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeletePrescriptionCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Guid> Handle(DeletePrescriptionCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Prescriptions.Remove(request.Id);
        await _unitOfWork.CompleteAsync();
        return result;
    }
}
