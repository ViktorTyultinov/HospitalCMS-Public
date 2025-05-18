using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Mapster;

namespace Hospital.Application.UseCases.GeneralPractitionerCheckUp.Commands;
public record UpdateGeneralPractitionerCheckUpCommand(Domain.Entities.MedicalHistory.GeneralPractitionerCheckUp CheckUp) : IRequest<GeneralPractitionerCheckUpDto>;
public class UpdateGeneralPractitionerCheckUpCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateGeneralPractitionerCheckUpCommand, GeneralPractitionerCheckUpDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<GeneralPractitionerCheckUpDto> Handle(UpdateGeneralPractitionerCheckUpCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.GeneralPractitionerCheckUps.Update(request.CheckUp);
        await _unitOfWork.CompleteAsync();
        return result.Adapt<GeneralPractitionerCheckUpDto>();
    }
}