using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Mapster;

namespace Hospital.Application.UseCases.Nurse.Commands;
public record UpdateNurseCommand(Domain.Entities.Users.NurseProfile Nurse) : IRequest<NurseDto>;

public class UpdateNurseCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateNurseCommand, NurseDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<NurseDto> Handle(UpdateNurseCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Nurses.Update(request.Nurse);
        await _unitOfWork.CompleteAsync();
        return result.Adapt<NurseDto>();
    }
}
