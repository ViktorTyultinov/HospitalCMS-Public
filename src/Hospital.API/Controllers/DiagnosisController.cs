using Hospital.Application.DTOs;
using Hospital.Application.UseCases.Diagnosis.Commands;
using Hospital.Application.UseCases.Diagnosis.Queries;
using Microsoft.AspNetCore.Mvc;
using Hospital.Application;

namespace Hospital.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DiagnosisController(IUseCaseExecutor executor) : ControllerBase
{
    private readonly IUseCaseExecutor _executor = executor;

    [HttpGet("get/{id}")]
    public ActionResult<DiagnosisDto> GetDiagnosisById(Guid id)
    {
        var response = _executor.Dispatch(new GetDiagnosisByIdQuery(id));
        return Ok(response.Result);
    }

    [HttpGet("get/all")]
    public ActionResult<IEnumerable<DiagnosisDto>> GetAllDiagnoses()
    {
        var response = _executor.Dispatch(new GetDiagnosisListQuery());
        return Ok(response.Result);
    }

    [HttpPost("add")]
    public ActionResult<Guid> AddDiagnosis([FromBody] AddDiagnosisCommand command)
    {
        var response = _executor.Dispatch(command);
        return Ok(response.Result);
    }

    [HttpPut("update")]
    public ActionResult<Guid> UpdateDiagnosis([FromBody] UpdateDiagnosisCommand command)
    {
        var response = _executor.Dispatch(command);
        return Ok(response.Result);
    }

    [HttpDelete("delete/{id}")]
    public ActionResult<Guid> DeleteDiagnosis(Guid id)
    {
        var response = _executor.Dispatch(new DeleteDiagnosisCommand(id));
        return Ok(response.Result);
    }
}