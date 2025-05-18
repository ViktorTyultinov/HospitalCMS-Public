using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Mapster;

namespace Hospital.Application.UseCases.PrescriptionLine.Queries;

public record GetPrescriptionLineListQuery() : IRequest<IEnumerable<PrescriptionLineDto>>;
public class GetPrescriptionLineListQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetPrescriptionLineListQuery, IEnumerable<PrescriptionLineDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<IEnumerable<PrescriptionLineDto>> Handle(GetPrescriptionLineListQuery request, CancellationToken cancellationToken)
    {
        var prescriptionLines = await _unitOfWork.PrescriptionLines.GetAllAsync();
        return prescriptionLines.Adapt<IEnumerable<PrescriptionLineDto>>();
    }
}