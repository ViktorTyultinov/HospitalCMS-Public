using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Mapster;

namespace Hospital.Application.UseCases.Department.Queries;
public record GetDepartmentListQuery() : IRequest<IEnumerable<DepartmentDto>>;
public class GetDepartmentListQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetDepartmentListQuery, IEnumerable<DepartmentDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<IEnumerable<DepartmentDto>> Handle(GetDepartmentListQuery request, CancellationToken cancellationToken)
    {
        var departments = await _unitOfWork.Departments.GetAllAsync() ?? [];
        return departments.Adapt<IEnumerable<DepartmentDto>>();
    }
}