using System.ComponentModel.DataAnnotations;
using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Mapster;

namespace Hospital.Application.UseCases.Hospital.Queries;

public record GetHospitalByIdQuery([Required] Guid Id) : IRequest<HospitalDto>;
public class GetHospitalByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetHospitalByIdQuery, HospitalDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<HospitalDto> Handle(GetHospitalByIdQuery request, CancellationToken cancellationToken)
    {
        var hospital = await _unitOfWork.Hospitals.GetByIdAsync(request.Id) ?? throw new Exception($"Hospital with ID {request.Id} not found.");
        return hospital.Adapt<HospitalDto>();
    }
}