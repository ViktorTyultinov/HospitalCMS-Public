using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Mapster;

namespace Hospital.Application.UseCases.PrescriptionLine.Queries;

public record GetPrescriptionLineByIdQuery(Guid Id) : IRequest<PrescriptionLineDto>;
public class GetPrescriptionLineByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetPrescriptionLineByIdQuery, PrescriptionLineDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<PrescriptionLineDto> Handle(GetPrescriptionLineByIdQuery request, CancellationToken cancellationToken)
    {
        var prescriptionLine = await _unitOfWork.PrescriptionLines.GetByIdAsync(request.Id)
            ?? throw new Exception($"Prescription Line with ID {request.Id} not found.");
        return prescriptionLine.Adapt<PrescriptionLineDto>();
    }
}