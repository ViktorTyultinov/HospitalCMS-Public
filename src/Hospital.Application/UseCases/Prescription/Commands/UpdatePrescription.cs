using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Mapster;

namespace Hospital.Application.UseCases.Prescription.Commands;

public record UpdatePrescriptionCommand(Domain.Entities.MedicalHistory.Prescription Prescription) : IRequest<PrescriptionDto>;
public class UpdatePrescriptionCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdatePrescriptionCommand, PrescriptionDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<PrescriptionDto> Handle(UpdatePrescriptionCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Prescriptions.Update(request.Prescription);
        await _unitOfWork.CompleteAsync();
        return result.Adapt<PrescriptionDto>();
    }
}