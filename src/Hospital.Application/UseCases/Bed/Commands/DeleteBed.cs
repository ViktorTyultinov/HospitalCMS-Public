using System.ComponentModel.DataAnnotations;
using Hospital.Application.Interfaces;

namespace Hospital.Application.UseCases.Bed.Commands;
public record DeleteBedCommand([Required] Guid Id) : IRequest<Guid>;
public class DeleteBedCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteBedCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Guid> Handle(DeleteBedCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Beds.Remove(request.Id);
        await _unitOfWork.CompleteAsync();
        return result;
    }
}