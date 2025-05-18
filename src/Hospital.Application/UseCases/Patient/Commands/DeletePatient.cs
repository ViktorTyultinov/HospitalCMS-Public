using System.ComponentModel.DataAnnotations;
using Hospital.Application.Interfaces;

namespace Hospital.Application.UseCases.Patient.Commands;

public record DeletePatientCommand([Required] Guid Id) : IRequest<Guid>;
public class DeletePatientCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeletePatientCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Guid> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Patients.Remove(request.Id);
        await _unitOfWork.CompleteAsync();
        return result;
    }
}