using System.ComponentModel.DataAnnotations;
using Hospital.Application.Interfaces;
using Hospital.Domain.Enums;

namespace Hospital.Application.UseCases.MedicalDevice.Commands;

public record AddMedicalDeviceCommand(
    [Required] string Name,
    [Required] string SerialNumber,
    [Required] DeviceType DeviceType,
    [Required] Guid HospitalId,
    Guid? StorageUnitId) : IRequest<Guid>;
public class AddMedicalDeviceCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddMedicalDeviceCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Guid> Handle(AddMedicalDeviceCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.MedicalDevices.AddAsync(new Domain.Entities.Devices.MedicalDevice
        {
            Name = request.Name,
            SerialNumber = request.SerialNumber,
            DeviceType = request.DeviceType,
            HospitalId = request.HospitalId,
        });

        await _unitOfWork.CompleteAsync();

        return result;
    }
}