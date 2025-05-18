using System.ComponentModel.DataAnnotations;
using Hospital.Application.Interfaces;

namespace Hospital.Application.UseCases.Bed.Commands;
public record AddBedCommand(
    [Required] int BedNumber,
    [Required] Guid RoomId,
    Guid? PatientId) : IRequest<Guid>;
public class AddBedCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddBedCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Guid> Handle(AddBedCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Beds.AddAsync(new Domain.Entities.Locations.Bed
        {
            BedNumber = request.BedNumber,
            RoomId = request.RoomId,
            PatientId = request.PatientId,
        });

        await _unitOfWork.CompleteAsync();

        return result;
    }
}