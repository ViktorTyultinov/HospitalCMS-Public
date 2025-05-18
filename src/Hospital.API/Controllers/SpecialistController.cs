using Hospital.Application.DTOs;
using Hospital.Application.UseCases.Specialist.Commands;
using Hospital.Application.UseCases.Specialist.Queries;
using Microsoft.AspNetCore.Mvc;
using Hospital.Application;

namespace Hospital.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SpecialistController(IUseCaseExecutor executor) : ControllerBase
{
    private readonly IUseCaseExecutor _executor = executor;

    [HttpGet("get/{id}")]
    public ActionResult<SpecialistDto> GetSpecialistById(Guid id)
    {
        var response = _executor.Dispatch(new GetSpecialistByIdQuery(id));
        return Ok(response.Result);
    }

    [HttpGet("get/all")]
    public ActionResult<IEnumerable<SpecialistDto>> GetAllSpecialists()
    {
        var response = _executor.Dispatch(new GetSpecialistListQuery());
        return Ok(response.Result);
    }

    [HttpPost("add")]
    public ActionResult<Guid> AddSpecialist([FromBody] AddSpecialistCommand command)
    {
        var response = _executor.Dispatch(command);
        return Ok(response.Result);
    }

    [HttpPut("update")]
    public ActionResult<Guid> UpdateSpecialist([FromBody] UpdateSpecialistCommand command)
    {
        var response = _executor.Dispatch(command);
        return Ok(response.Result);
    }

    [HttpDelete("delete/{id}")]
    public ActionResult<Guid> DeleteSpecialist(Guid id)
    {
        var response = _executor.Dispatch(new DeleteSpecialistCommand(id));
        return Ok(response.Result);
    }
}