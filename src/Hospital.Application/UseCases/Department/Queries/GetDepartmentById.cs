using System.ComponentModel.DataAnnotations;
using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Mapster;

namespace Hospital.Application.UseCases.Department.Queries;
public record GetDepartmentByIdQuery([Required] Guid Id) : IRequest<DepartmentDto>;
public class GetDepartmentByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetDepartmentByIdQuery, DepartmentDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<DepartmentDto> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
    {
        var department = await _unitOfWork.Departments.GetByIdAsync(request.Id) ?? throw new Exception($"Department with ID {request.Id} not found.");
        return department.Adapt<DepartmentDto>();
    }
}