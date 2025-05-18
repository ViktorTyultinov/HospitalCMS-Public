using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Mapster;

namespace Hospital.Application.UseCases.Prescription.Queries;

public record GetPrescriptionListQuery() : IRequest<IEnumerable<PrescriptionDto>>;
public class GetPrescriptionListQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetPrescriptionListQuery, IEnumerable<PrescriptionDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<IEnumerable<PrescriptionDto>> Handle(GetPrescriptionListQuery request, CancellationToken cancellationToken)
    {
        var prescriptions = await _unitOfWork.Prescriptions.GetAllAsync();
        return prescriptions.Adapt<IEnumerable<PrescriptionDto>>();
    }
}