using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Mapster;

namespace Hospital.Application.UseCases.GeneralPractitionerCheckUp.Queries;

public record GetGeneralPractitionerCheckUpByIdQuery(Guid Id) : IRequest<GeneralPractitionerCheckUpDto>;
public class GetGeneralPractitionerCheckUpByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetGeneralPractitionerCheckUpByIdQuery, GeneralPractitionerCheckUpDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<GeneralPractitionerCheckUpDto> Handle(GetGeneralPractitionerCheckUpByIdQuery request, CancellationToken cancellationToken)
    {
        var checkUp = await _unitOfWork.GeneralPractitionerCheckUps.GetByIdAsync(request.Id)
            ?? throw new Exception($"CheckUp with ID {request.Id} not found.");
        return checkUp.Adapt<GeneralPractitionerCheckUpDto>();
    }
}