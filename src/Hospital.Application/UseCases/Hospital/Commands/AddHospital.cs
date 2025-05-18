using System.ComponentModel.DataAnnotations;
using Hospital.Application.Interfaces;

namespace Hospital.Application.UseCases.Hospital.Commands;

public record AddHospitalCommand(
    [Required] string Name,
    [Required] string Address) : IRequest<Guid>;

public class AddHospital(IUnitOfWork unitOfWork) : IRequestHandler<AddHospitalCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Guid> Handle(AddHospitalCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Hospitals.AddAsync(new Domain.Entities.Locations.Hospital
        {
            Name = request.Name,
            Address = request.Address,
        });

        await _unitOfWork.CompleteAsync();

        return result;
    }
}