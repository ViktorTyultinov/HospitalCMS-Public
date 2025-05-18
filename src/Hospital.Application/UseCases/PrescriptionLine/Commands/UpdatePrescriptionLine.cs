using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Mapster;

namespace Hospital.Application.UseCases.PrescriptionLine.Commands;

public record UpdatePrescriptionLineCommand(Domain.Entities.MedicalHistory.PrescriptionLine PrescriptionLine) : IRequest<PrescriptionLineDto>;
public class UpdatePrescriptionLineCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdatePrescriptionLineCommand, PrescriptionLineDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<PrescriptionLineDto> Handle(UpdatePrescriptionLineCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.PrescriptionLines.Update(request.PrescriptionLine);
        await _unitOfWork.CompleteAsync();
        return result.Adapt<PrescriptionLineDto>();
    }
}