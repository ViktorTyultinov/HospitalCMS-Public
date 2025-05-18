using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Mapster;

namespace Hospital.Application.UseCases.Patient.Queries;

public record GetPatientByIdQuery(Guid Id) : IRequest<PatientDto>;
public class GetPatientByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetPatientByIdQuery, PatientDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<PatientDto> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
    {
        var patient = await _unitOfWork.Patients.GetByIdAsync(request.Id) ?? throw new Exception($"Patient with ID {request.Id} not found.");
        return patient.Adapt<PatientDto>();
    }
}