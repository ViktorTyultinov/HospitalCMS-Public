using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Mapster;

namespace Hospital.Application.UseCases.MedicalDevice.Queries;

public record GetMedicalDeviceByIdQuery(Guid Id) : IRequest<MedicalDeviceDto>;
public class GetMedicalDeviceByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetMedicalDeviceByIdQuery, MedicalDeviceDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<MedicalDeviceDto> Handle(GetMedicalDeviceByIdQuery request, CancellationToken cancellationToken)
    {
        var medicalDevice = await _unitOfWork.MedicalDevices.GetByIdAsync(request.Id)
            ?? throw new Exception($"Medical Device with ID {request.Id} not found.");
        return medicalDevice.Adapt<MedicalDeviceDto>();
    }
}