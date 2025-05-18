using Hospital.Application.DTOs;
using Hospital.Application.UseCases.PrescriptionLine.Commands;
using Hospital.Application.UseCases.PrescriptionLine.Queries;
using Microsoft.AspNetCore.Mvc;
using Hospital.Application;

namespace Hospital.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PrescriptionLineController(IUseCaseExecutor executor) : ControllerBase
{
    private readonly IUseCaseExecutor _executor = executor;

    [HttpGet("get/{id}")]
    public ActionResult<PrescriptionLineDto> GetPrescriptionLineById(Guid id)
    {
        var response = _executor.Dispatch(new GetPrescriptionLineByIdQuery(id));
        return Ok(response.Result);
    }

    [HttpGet("get/all")]
    public ActionResult<IEnumerable<PrescriptionLineDto>> GetAllPrescriptionLines()
    {
        var response = _executor.Dispatch(new GetPrescriptionLineListQuery());
        return Ok(response.Result);
    }

    [HttpPost("add")]
    public ActionResult<Guid> AddPrescriptionLine([FromBody] AddPrescriptionLineCommand command)
    {
        var response = _executor.Dispatch(command);
        return Ok(response.Result);
    }

    [HttpPut("update")]
    public ActionResult<Guid> UpdatePrescriptionLine([FromBody] UpdatePrescriptionLineCommand command)
    {
        var response = _executor.Dispatch(command);
        return Ok(response.Result);
    }

    [HttpDelete("delete/{id}")]
    public ActionResult<Guid> DeletePrescriptionLine(Guid id)
    {
        var response = _executor.Dispatch(new DeletePrescriptionLineCommand(id));
        return Ok(response.Result);
    }
}