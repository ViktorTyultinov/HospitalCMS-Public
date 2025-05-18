using Hospital.Application.DTOs;
using Hospital.Application.UseCases.Nurse.Commands;
using Hospital.Application.UseCases.Nurse.Queries;
using Microsoft.AspNetCore.Mvc;
using Hospital.Application;

namespace Hospital.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NurseController(IUseCaseExecutor executor) : ControllerBase
{
    private readonly IUseCaseExecutor _executor = executor;

    [HttpGet("get/{id}")]
    public ActionResult<NurseDto> GetNurseById(Guid id)
    {
        var response = _executor.Dispatch(new GetNurseByIdQuery(id));
        return Ok(response.Result);
    }

    [HttpGet("get/all")]
    public ActionResult<IEnumerable<NurseDto>> GetAllNurses()
    {
        var response = _executor.Dispatch(new GetNurseListQuery());
        return Ok(response.Result);
    }

    [HttpPost("add")]
    public ActionResult<Guid> AddNurse([FromBody] AddNurseCommand command)
    {
        var response = _executor.Dispatch(command);
        return Ok(response.Result);
    }

    [HttpPut("update")]
    public ActionResult<Guid> UpdateNurse([FromBody] UpdateNurseCommand command)
    {
        var response = _executor.Dispatch(command);
        return Ok(response.Result);
    }

    [HttpDelete("delete/{id}")]
    public ActionResult<Guid> DeleteNurse(Guid id)
    {
        var response = _executor.Dispatch(new DeleteNurseCommand(id));
        return Ok(response.Result);
    }
}