using System.ComponentModel.DataAnnotations;
using Hospital.Application.Interfaces;

namespace Hospital.Application.UseCases.Department.Commands;
public record AddDepartmentCommand(
    [Required] string Name,
    [Required] string Description,
    [Required] string Address,
    [Required] Guid HospitalId) : IRequest<Guid>;
public class AddDepartmentCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddDepartmentCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Guid> Handle(AddDepartmentCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Departments.AddAsync(new Domain.Entities.Locations.Department
        {
            Name = request.Name,
            Description = request.Description,
            Address = request.Address,
            HospitalId = request.HospitalId,
            StorageUnits = [],
            Rooms = [],
        });

        await _unitOfWork.CompleteAsync();

        return result;
    }
}