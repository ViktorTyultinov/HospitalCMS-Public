using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Mapster;

namespace Hospital.Application.UseCases.Department.Commands;
public record UpdateDepartmentCommand(Domain.Entities.Locations.Department Department) : IRequest<DepartmentDto>;
public class UpdateDepartmentCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateDepartmentCommand, DepartmentDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<DepartmentDto> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Departments.Update(request.Department);
        await _unitOfWork.CompleteAsync();
        return result.Adapt<DepartmentDto>();
    }
}