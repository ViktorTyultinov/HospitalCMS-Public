using System.ComponentModel.DataAnnotations;
using Hospital.Application.Interfaces;

namespace Hospital.Application.UseCases.Nurse.Commands;

public record DeleteNurseCommand([Required] Guid Id) : IRequest<Guid>;
public class DeleteNurseCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteNurseCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Guid> Handle(DeleteNurseCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Nurses.Remove(request.Id);
        await _unitOfWork.CompleteAsync();
        return result;
    }
}