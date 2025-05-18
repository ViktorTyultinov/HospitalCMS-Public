using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Mapster;

namespace Hospital.Application.UseCases.StorageUnit.Commands;

public record UpdateStorageUnitCommand(Domain.Entities.Locations.StorageUnit StorageUnit) : IRequest<StorageUnitDto>;
public class UpdateStorageUnitCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateStorageUnitCommand, StorageUnitDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<StorageUnitDto> Handle(UpdateStorageUnitCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.StorageUnits.Update(request.StorageUnit);
        await _unitOfWork.CompleteAsync();
        return result.Adapt<StorageUnitDto>();
    }
}