using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Mapster;

namespace Hospital.Application.UseCases.Nurse.Queries;

public record GetNurseListQuery() : IRequest<IEnumerable<NurseDto>>;
public class GetNurseListQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetNurseListQuery, IEnumerable<NurseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<IEnumerable<NurseDto>> Handle(GetNurseListQuery request, CancellationToken cancellationToken)
    {
        var nurses = await _unitOfWork.Nurses.GetAllAsync();
        return nurses.Adapt<IEnumerable<NurseDto>>();
    }
}
