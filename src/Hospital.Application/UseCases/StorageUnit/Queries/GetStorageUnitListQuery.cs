using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Mapster;

namespace Hospital.Application.UseCases.StorageUnit.Queries;

public record GetStorageUnitListQuery() : IRequest<IEnumerable<StorageUnitDto>>;
public class GetStorageUnitListQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetStorageUnitListQuery, IEnumerable<StorageUnitDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<IEnumerable<StorageUnitDto>> Handle(GetStorageUnitListQuery request, CancellationToken cancellationToken)
    {
        var storageUnits = await _unitOfWork.StorageUnits.GetAllAsync();
        return storageUnits.Adapt<IEnumerable<StorageUnitDto>>();
    }
}