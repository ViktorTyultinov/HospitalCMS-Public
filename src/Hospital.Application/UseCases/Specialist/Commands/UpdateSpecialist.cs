using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Mapster;

namespace Hospital.Application.UseCases.Specialist.Commands;

public record UpdateSpecialistCommand(Domain.Entities.Users.SpecialistProfile Specialist) : IRequest<SpecialistDto>;
public class UpdateSpecialistCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateSpecialistCommand, SpecialistDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<SpecialistDto> Handle(UpdateSpecialistCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Specialists.Update(request.Specialist);
        await _unitOfWork.CompleteAsync();
        return result.Adapt<SpecialistDto>();
    }
}