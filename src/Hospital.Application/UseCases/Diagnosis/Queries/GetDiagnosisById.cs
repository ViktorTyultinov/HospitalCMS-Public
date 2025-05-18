using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Mapster;

namespace Hospital.Application.UseCases.Diagnosis.Queries;

public record GetDiagnosisByIdQuery(Guid Id) : IRequest<DiagnosisDto>;
public class GetDiagnosisByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetDiagnosisByIdQuery, DiagnosisDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<DiagnosisDto> Handle(GetDiagnosisByIdQuery request, CancellationToken cancellationToken)
    {
        var diagnosis = await _unitOfWork.Diagnoses.GetByIdAsync(request.Id)
            ?? throw new Exception($"Diagnosis with ID {request.Id} not found.");
        return diagnosis.Adapt<DiagnosisDto>();
    }
}