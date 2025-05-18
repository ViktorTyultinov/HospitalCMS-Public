using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Mapster;

namespace Hospital.Application.UseCases.Diagnosis.Queries;

public record GetDiagnosisListQuery() : IRequest<IEnumerable<DiagnosisDto>>;
public class GetDiagnosisListQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetDiagnosisListQuery, IEnumerable<DiagnosisDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<IEnumerable<DiagnosisDto>> Handle(GetDiagnosisListQuery request, CancellationToken cancellationToken)
    {
        var diagnoses = await _unitOfWork.Diagnoses.GetAllAsync();
        return diagnoses.Adapt<IEnumerable<DiagnosisDto>>();
    }
}