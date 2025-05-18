using System.ComponentModel.DataAnnotations;
using Hospital.Application.Interfaces;

namespace Hospital.Application.UseCases.StorageUnit.Commands;

public record AddStorageUnitCommand(
    [Required] string Name,
    [Required] string Description,
    [Required] string Address,
    [Required] Guid DepartmentId) : IRequest<Guid>;
public class AddStorageUnitCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddStorageUnitCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Guid> Handle(AddStorageUnitCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.StorageUnits.AddAsync(new Domain.Entities.Locations.StorageUnit
        {
            Name = request.Name,
            Description = request.Description,
            Address = request.Address,
            DepartmentId = request.DepartmentId
        });

        await _unitOfWork.CompleteAsync();

        return result;
    }
}