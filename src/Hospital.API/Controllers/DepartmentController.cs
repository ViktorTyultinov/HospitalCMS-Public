using Hospital.Application.DTOs;
using Hospital.Application.UseCases.Department.Commands;
using Hospital.Application.UseCases.Department.Queries;
using Microsoft.AspNetCore.Mvc;
using Hospital.Application;

namespace Hospital.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentController(IUseCaseExecutor executor) : ControllerBase
{
    private readonly IUseCaseExecutor _executor = executor;

    [HttpGet("get/{id}")]
    public ActionResult<DepartmentDto> GetDepartmentById(Guid id)
    {
        var response = _executor.Dispatch(new GetDepartmentByIdQuery(id));
        return Ok(response.Result);
    }

    [HttpGet("get/all")]
    public ActionResult<IEnumerable<DepartmentDto>> GetAllDepartments()
    {
        var response = _executor.Dispatch(new GetDepartmentListQuery());
        return Ok(response.Result);
    }

    [HttpPost("add")]
    public ActionResult<Guid> AddDepartment([FromBody] AddDepartmentCommand command)
    {
        var response = _executor.Dispatch(command);
        return Ok(response.Result);
    }

    [HttpPut("update")]
    public ActionResult<Guid> UpdateDepartment([FromBody] UpdateDepartmentCommand command)
    {
        var response = _executor.Dispatch(command);
        return Ok(response.Result);
    }

    [HttpDelete("delete/{id}")]
    public ActionResult<Guid> DeleteDepartment(Guid id)
    {
        var response = _executor.Dispatch(new DeleteDepartmentCommand(id));
        return Ok(response.Result);
    }
}