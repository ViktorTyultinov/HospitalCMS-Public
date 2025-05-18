using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Mapster;

namespace Hospital.Application.UseCases.Bed.Queries;
public record GetBedListQuery() : IRequest<IEnumerable<BedDto>>;
public class GetBedListQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetBedListQuery, IEnumerable<BedDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IEnumerable<BedDto>> Handle(GetBedListQuery request, CancellationToken cancellationToken)
    {
        var beds = await _unitOfWork.Beds.GetAllAsync() ?? [];
        return beds.Adapt<IEnumerable<BedDto>>();
    }
}