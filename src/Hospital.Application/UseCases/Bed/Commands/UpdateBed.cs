using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Mapster;

namespace Hospital.Application.UseCases.Bed.Commands;
public record UpdateBedCommand(Domain.Entities.Locations.Bed Bed) : IRequest<BedDto>;
public class UpdateBedCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateBedCommand, BedDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<BedDto> Handle(UpdateBedCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Beds.Update(request.Bed);
        await _unitOfWork.CompleteAsync();
        return result.Adapt<BedDto>();
    }
}