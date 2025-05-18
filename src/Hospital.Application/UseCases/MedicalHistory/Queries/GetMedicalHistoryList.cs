using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;

namespace Hospital.Application.UseCases.MedicalHistory.Queries;

public record GetMedicalHistoryListQuery : IRequest<IEnumerable<MedicalHistoryDto>>;
public class GetMedicalHistoryListQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetMedicalHistoryListQuery, IEnumerable<MedicalHistoryDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<IEnumerable<MedicalHistoryDto>> Handle(GetMedicalHistoryListQuery request, CancellationToken cancellationToken)
    {
        var medicalHistories = await _unitOfWork.MedicalHistories.GetAllAsync();
        return medicalHistories.Select(mh => new MedicalHistoryDto(mh.Id, mh.Patient!.Id, mh.Events.Select(e => e.Id)));
    }
}