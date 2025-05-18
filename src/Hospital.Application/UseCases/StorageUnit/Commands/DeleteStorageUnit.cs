using System.ComponentModel.DataAnnotations;
using Hospital.Application.Interfaces;

namespace Hospital.Application.UseCases.StorageUnit.Commands;

public record DeleteStorageUnitCommand([Required] Guid Id) : IRequest<Guid>;
public class DeleteStorageUnitCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteStorageUnitCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Guid> Handle(DeleteStorageUnitCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.StorageUnits.Remove(request.Id);
        await _unitOfWork.CompleteAsync();
        return result;
    }
}