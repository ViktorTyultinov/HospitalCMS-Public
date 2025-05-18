using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Mapster;

namespace Hospital.Application.UseCases.MedicalDevice.Commands;

public record UpdateMedicalDeviceCommand(Domain.Entities.Devices.MedicalDevice MedicalDevice) : IRequest<MedicalDeviceDto>;

public class UpdateMedicalDeviceCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateMedicalDeviceCommand, MedicalDeviceDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<MedicalDeviceDto> Handle(UpdateMedicalDeviceCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.MedicalDevices.Update(request.MedicalDevice);
        await _unitOfWork.CompleteAsync();
        return result.Adapt<MedicalDeviceDto>();
    }
}