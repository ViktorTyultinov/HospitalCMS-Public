using System.ComponentModel.DataAnnotations;
using Hospital.Application.Interfaces;

namespace Hospital.Application.UseCases.GeneralPractitionerCheckUp.Commands;

public record DeleteGeneralPractitionerCheckUpCommand([Required] Guid Id) : IRequest<Guid>;
public class DeleteGeneralPractitionerCheckUpCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteGeneralPractitionerCheckUpCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Guid> Handle(DeleteGeneralPractitionerCheckUpCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.GeneralPractitionerCheckUps.Remove(request.Id);
        await _unitOfWork.CompleteAsync();
        return result;
    }
}