using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Mapster;

namespace Hospital.Application.UseCases.Room.Queries;

public record GetRoomByIdQuery(Guid Id) : IRequest<RoomDto>;
public class GetRoomByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetRoomByIdQuery, RoomDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<RoomDto> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
    {
        var room = await _unitOfWork.Rooms.GetByIdAsync(request.Id) ?? throw new Exception($"Room with ID {request.Id} not found.");
        return room.Adapt<RoomDto>();
    }
}