using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Mapster;

namespace Hospital.Application.UseCases.Hospital.Commands;

public record UpdateHospitalCommand(Domain.Entities.Locations.Hospital Hospital) : IRequest<HospitalDto>;
public class UpdateHospital(IUnitOfWork unitOfWork) : IRequestHandler<UpdateHospitalCommand, HospitalDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<HospitalDto> Handle(UpdateHospitalCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Hospitals.Update(request.Hospital);
        await _unitOfWork.CompleteAsync();
        return result.Adapt<HospitalDto>();
    }
}