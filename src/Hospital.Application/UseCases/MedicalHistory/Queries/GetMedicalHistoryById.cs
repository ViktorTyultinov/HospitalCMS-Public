using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;

namespace Hospital.Application.UseCases.MedicalHistory.Queries;

public record GetMedicalHistoryByIdQuery(Guid Id) : IRequest<MedicalHistoryDto>;
public class GetMedicalHistoryByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetMedicalHistoryByIdQuery, MedicalHistoryDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<MedicalHistoryDto> Handle(GetMedicalHistoryByIdQuery request, CancellationToken cancellationToken)
    {
        var medicalHistory = await _unitOfWork.MedicalHistories.GetByIdAsync(request.Id) ??
            throw new KeyNotFoundException("Medical history not found.");
        return new MedicalHistoryDto(medicalHistory.Id, medicalHistory.Patient!.Id, medicalHistory.Events.Select(e => e.Id));
    }
}
