using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Mapster;

namespace Hospital.Application.UseCases.GeneralPractitionerCheckUp.Queries;

public record GetGeneralPractitionerCheckUpListQuery() : IRequest<IEnumerable<GeneralPractitionerCheckUpDto>>;
public class GetGeneralPractitionerCheckUpListQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetGeneralPractitionerCheckUpListQuery, IEnumerable<GeneralPractitionerCheckUpDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<IEnumerable<GeneralPractitionerCheckUpDto>> Handle(GetGeneralPractitionerCheckUpListQuery request, CancellationToken cancellationToken)
    {
        var checkUps = await _unitOfWork.GeneralPractitionerCheckUps.GetAllAsync();
        return checkUps.Adapt<IEnumerable<GeneralPractitionerCheckUpDto>>();
    }
}