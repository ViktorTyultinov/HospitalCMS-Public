using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Mapster;

namespace Hospital.Application.UseCases.Patient.Queries;

public record GetPatientListQuery() : IRequest<IEnumerable<PatientDto>>;
public class GetPatientListQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetPatientListQuery, IEnumerable<PatientDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<IEnumerable<PatientDto>> Handle(GetPatientListQuery request, CancellationToken cancellationToken)
    {
        var patients = await _unitOfWork.Patients.GetAllAsync();
        return patients.Adapt<IEnumerable<PatientDto>>();
    }
}
