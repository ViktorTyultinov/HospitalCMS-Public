using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Mapster;

namespace Hospital.Application.UseCases.Room.Commands;

public record UpdateRoomCommand(Domain.Entities.Locations.Room Room) : IRequest<RoomDto>;
public class UpdateRoomCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateRoomCommand, RoomDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<RoomDto> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Rooms.Update(request.Room);
        await _unitOfWork.CompleteAsync();
        return result.Adapt<RoomDto>();
    }
}