using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Mapster;

namespace Hospital.Application.UseCases.Room.Queries;

public record GetRoomListQuery() : IRequest<IEnumerable<RoomDto>>;
public class GetRoomListQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetRoomListQuery, IEnumerable<RoomDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<IEnumerable<RoomDto>> Handle(GetRoomListQuery request, CancellationToken cancellationToken)
    {
        var rooms = await _unitOfWork.Rooms.GetAllAsync();
        return rooms.Adapt<IEnumerable<RoomDto>>();
    }
}