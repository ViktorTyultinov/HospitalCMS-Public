using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Mapster;

namespace Hospital.Application.UseCases.Specialist.Queries;

public record GetSpecialistByIdQuery(Guid Id) : IRequest<SpecialistDto>;
public class GetSpecialistByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetSpecialistByIdQuery, SpecialistDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<SpecialistDto> Handle(GetSpecialistByIdQuery request, CancellationToken cancellationToken)
    {
        var specialist = await _unitOfWork.Specialists.GetByIdAsync(request.Id) ?? throw new Exception($"Specialist with ID {request.Id} not found.");
        return specialist.Adapt<SpecialistDto>();
    }
}