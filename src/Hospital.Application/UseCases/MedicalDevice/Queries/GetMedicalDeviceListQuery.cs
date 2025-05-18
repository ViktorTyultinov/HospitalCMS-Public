using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Mapster;

namespace Hospital.Application.UseCases.MedicalDevice.Queries;

public record GetMedicalDeviceListQuery() : IRequest<IEnumerable<MedicalDeviceDto>>;
public class GetMedicalDeviceListQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetMedicalDeviceListQuery, IEnumerable<MedicalDeviceDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<IEnumerable<MedicalDeviceDto>> Handle(GetMedicalDeviceListQuery request, CancellationToken cancellationToken)
    {
        var medicalDevices = await _unitOfWork.MedicalDevices.GetAllAsync();
        return medicalDevices.Adapt<IEnumerable<MedicalDeviceDto>>();
    }
}
