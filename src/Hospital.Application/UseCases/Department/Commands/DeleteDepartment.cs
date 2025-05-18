using System.ComponentModel.DataAnnotations;
using Hospital.Application.Interfaces;

namespace Hospital.Application.UseCases.Department.Commands;
public record DeleteDepartmentCommand([Required] Guid Id) : IRequest<Guid>;
public class DeleteDepartmentCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteDepartmentCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Guid> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Departments.Remove(request.Id);
        await _unitOfWork.CompleteAsync();
        return result;
    }
}