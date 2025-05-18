using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Mapster;

namespace Hospital.Application.UseCases.StorageUnit.Queries;
public record GetStorageUnitByIdQuery(Guid Id) : IRequest<StorageUnitDto>;
public class GetStorageUnitByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetStorageUnitByIdQuery, StorageUnitDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<StorageUnitDto> Handle(GetStorageUnitByIdQuery request, CancellationToken cancellationToken)
    {
        var storageUnit = await _unitOfWork.StorageUnits.GetByIdAsync(request.Id)
            ?? throw new Exception($"Storage Unit with ID {request.Id} not found.");
        return storageUnit.Adapt<StorageUnitDto>();
    }
}