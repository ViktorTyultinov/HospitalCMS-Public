using Hospital.Application.DTOs;
using Hospital.Application.UseCases.GeneralPractitionerCheckUp.Commands;
using Hospital.Application.UseCases.GeneralPractitionerCheckUp.Queries;
using Microsoft.AspNetCore.Mvc;
using Hospital.Application;

namespace Hospital.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GeneralPractitionerCheckUpController(IUseCaseExecutor executor) : ControllerBase
{
    private readonly IUseCaseExecutor _executor = executor;

    [HttpGet("get/{id}")]
    public ActionResult<GeneralPractitionerCheckUpDto> GetCheckUpById(Guid id)
    {
        var response = _executor.Dispatch(new GetGeneralPractitionerCheckUpByIdQuery(id));
        return Ok(response.Result);
    }

    [HttpGet("get/all")]
    public ActionResult<IEnumerable<GeneralPractitionerCheckUpDto>> GetAllCheckUps()
    {
        var response = _executor.Dispatch(new GetGeneralPractitionerCheckUpListQuery());
        return Ok(response.Result);
    }

    [HttpPost("add")]
    public ActionResult<Guid> AddCheckUp([FromBody] AddGeneralPractitionerCheckUpCommand command)
    {
        var response = _executor.Dispatch(command);
        return Ok(response.Result);
    }

    [HttpPut("update")]
    public ActionResult<Guid> UpdateCheckUp([FromBody] UpdateGeneralPractitionerCheckUpCommand command)
    {
        var response = _executor.Dispatch(command);
        return Ok(response.Result);
    }

    [HttpDelete("delete/{id}")]
    public ActionResult<Guid> DeleteCheckUp(Guid id)
    {
        var response = _executor.Dispatch(new DeleteGeneralPractitionerCheckUpCommand(id));
        return Ok(response.Result);
    }
}