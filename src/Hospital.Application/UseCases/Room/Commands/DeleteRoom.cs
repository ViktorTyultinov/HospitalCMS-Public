using System.ComponentModel.DataAnnotations;
using Hospital.Application.Interfaces;

namespace Hospital.Application.UseCases.Room.Commands;

public record DeleteRoomCommand([Required] Guid Id) : IRequest<Guid>;
public class DeleteRoomCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteRoomCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Guid> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Rooms.Remove(request.Id);
        await _unitOfWork.CompleteAsync();
        return result;
    }
}