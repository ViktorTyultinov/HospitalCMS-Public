using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Mapster;

namespace Hospital.Application.UseCases.Hospital.Queries;
public record GetHospitalListQuery() : IRequest<IEnumerable<HospitalDto>>;
public class GetHospitalListQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetHospitalListQuery, IEnumerable<HospitalDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<IEnumerable<HospitalDto>> Handle(GetHospitalListQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Domain.Entities.Locations.Hospital> hospitals = await _unitOfWork.Hospitals.GetAllAsync() ?? [];
        return hospitals.Adapt<IEnumerable<HospitalDto>>();
    }
}