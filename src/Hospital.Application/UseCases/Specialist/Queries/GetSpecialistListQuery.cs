using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Mapster;

namespace Hospital.Application.UseCases.Specialist.Queries;

public record GetSpecialistListQuery() : IRequest<IEnumerable<SpecialistDto>>;
public class GetSpecialistListQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetSpecialistListQuery, IEnumerable<SpecialistDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<IEnumerable<SpecialistDto>> Handle(GetSpecialistListQuery request, CancellationToken cancellationToken)
    {
        var specialists = await _unitOfWork.Specialists.GetAllAsync();
        return specialists.Adapt<IEnumerable<SpecialistDto>>();
    }
}
