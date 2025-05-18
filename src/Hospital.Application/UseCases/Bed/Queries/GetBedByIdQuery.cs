using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Mapster;

namespace Hospital.Application.UseCases.Bed.Queries;
public record GetBedByIdQuery(Guid Id) : IRequest<BedDto>;

public class GetBedByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetBedByIdQuery, BedDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<BedDto> Handle(GetBedByIdQuery request, CancellationToken cancellationToken)
    {
        var bed = await _unitOfWork.Beds.GetByIdAsync(request.Id)
            ?? throw new Exception($"Bed with ID {request.Id} not found.");
        return bed.Adapt<BedDto>();
    }
}