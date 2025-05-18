using Hospital.Application.DTOs;
using Hospital.Application.UseCases.MedicalHistory.Commands;
using Hospital.Application.UseCases.MedicalHistory.Queries;
using Microsoft.AspNetCore.Mvc;
using Hospital.Application;

namespace Hospital.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MedicalHistoryController(IUseCaseExecutor executor) : ControllerBase
{
    private readonly IUseCaseExecutor _executor = executor;

    [HttpGet("get/{id}")]
    public ActionResult<MedicalHistoryDto> GetMedicalHistoryById(Guid id)
    {
        var response = _executor.Dispatch(new GetMedicalHistoryByIdQuery(id));
        return Ok(response.Result);
    }

    [HttpGet("get/all")]
    public ActionResult<IEnumerable<MedicalHistoryDto>> GetAllMedicalHistories()
    {
        var response = _executor.Dispatch(new GetMedicalHistoryListQuery());
        return Ok(response.Result);
    }

    [HttpPost("add")]
    public ActionResult<Guid> AddMedicalHistory([FromBody] AddMedicalHistoryCommand command)
    {
        var response = _executor.Dispatch(command);
        return Ok(response.Result);
    }

    [HttpPut("update")]
    public ActionResult<Guid> UpdateMedicalHistory([FromBody] UpdateMedicalHistoryCommand command)
    {
        var response = _executor.Dispatch(command);
        return Ok(response.Result);
    }

    [HttpDelete("delete/{id}")]
    public ActionResult<Guid> DeleteMedicalHistory(Guid id)
    {
        var response = _executor.Dispatch(new DeleteMedicalHistoryCommand(id));
        return Ok(response.Result);
    }
}
