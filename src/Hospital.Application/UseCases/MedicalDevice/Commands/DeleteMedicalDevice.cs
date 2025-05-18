using System.ComponentModel.DataAnnotations;
using Hospital.Application.Interfaces;

namespace Hospital.Application.UseCases.MedicalDevice.Commands;

public record DeleteMedicalDeviceCommand([Required] Guid Id) : IRequest<Guid>;
public class DeleteMedicalDeviceCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteMedicalDeviceCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Guid> Handle(DeleteMedicalDeviceCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.MedicalDevices.Remove(request.Id);
        await _unitOfWork.CompleteAsync();
        return result;
    }
}