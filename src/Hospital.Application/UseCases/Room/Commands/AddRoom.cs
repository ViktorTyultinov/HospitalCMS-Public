using System.ComponentModel.DataAnnotations;
using Hospital.Application.Interfaces;

namespace Hospital.Application.UseCases.Room.Commands;

public record AddRoomCommand(
    [Required] string Name,
    [Required] string Description,
    [Required] string Address,
    [Required] int RoomNumber,
    [Required] int Floor,
    [Required] Guid DepartmentId) : IRequest<Guid>;
public class AddRoomCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddRoomCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Guid> Handle(AddRoomCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Rooms.AddAsync(new Domain.Entities.Locations.Room
        {
            Name = request.Name,
            Description = request.Description,
            Address = request.Address,
            RoomNumber = request.RoomNumber,
            Floor = request.Floor,
            DepartmentId = request.DepartmentId,
        });

        await _unitOfWork.CompleteAsync();

        return result;
    }
}