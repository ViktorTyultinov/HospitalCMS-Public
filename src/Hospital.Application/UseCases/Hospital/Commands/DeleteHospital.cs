using System.ComponentModel.DataAnnotations;
using Hospital.Application.Interfaces;

namespace Hospital.Application.UseCases.Hospital.Commands;

public record DeleteHospitalCommand([Required] Guid Id) : IRequest<Guid>;
public class DeleteHospital(IUnitOfWork unitOfWork) : IRequestHandler<DeleteHospitalCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Guid> Handle(DeleteHospitalCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Hospitals.Remove(request.Id);
        await _unitOfWork.CompleteAsync();
        return result;
    }
}