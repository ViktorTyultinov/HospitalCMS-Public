using System.ComponentModel.DataAnnotations;
using Hospital.Application.Interfaces;

namespace Hospital.Application.UseCases.Specialist.Commands;

public record DeleteSpecialistCommand([Required] Guid Id) : IRequest<Guid>;
public class DeleteSpecialistCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteSpecialistCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Guid> Handle(DeleteSpecialistCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Specialists.Remove(request.Id);
        await _unitOfWork.CompleteAsync();
        return result;
    }
}