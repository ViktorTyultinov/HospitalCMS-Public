using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Mapster;

namespace Hospital.Application.UseCases.Nurse.Queries;

public record GetNurseByIdQuery(Guid Id) : IRequest<NurseDto>;
public class GetNurseByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetNurseByIdQuery, NurseDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<NurseDto> Handle(GetNurseByIdQuery request, CancellationToken cancellationToken)
    {
        var nurse = await _unitOfWork.Nurses.GetByIdAsync(request.Id) ?? throw new Exception($"Nurse with ID {request.Id} not found.");
        return nurse.Adapt<NurseDto>();
    }
}