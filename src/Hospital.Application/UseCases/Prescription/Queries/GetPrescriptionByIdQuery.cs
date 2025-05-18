using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Mapster;

namespace Hospital.Application.UseCases.Prescription.Queries;

public record GetPrescriptionByIdQuery(Guid Id) : IRequest<PrescriptionDto>;
public class GetPrescriptionByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetPrescriptionByIdQuery, PrescriptionDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<PrescriptionDto> Handle(GetPrescriptionByIdQuery request, CancellationToken cancellationToken)
    {
        var prescription = await _unitOfWork.Prescriptions.GetByIdAsync(request.Id)
            ?? throw new Exception($"Prescription with ID {request.Id} not found.");
        return prescription.Adapt<PrescriptionDto>();
    }
}