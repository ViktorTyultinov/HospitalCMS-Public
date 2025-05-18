using Hospital.Application.DTOs;
using Hospital.Application.UseCases.Patient.Commands;
using Hospital.Application.UseCases.Patient.Queries;
using Microsoft.AspNetCore.Mvc;
using Hospital.Application;

namespace Hospital.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientController(IUseCaseExecutor executor) : ControllerBase
{
    private readonly IUseCaseExecutor _executor = executor;

    [HttpGet("get/{id}")]
    public ActionResult<PatientDto> GetPatientById(Guid id)
    {
        var response = _executor.Dispatch(new GetPatientByIdQuery(id));
        return Ok(response.Result);
    }

    [HttpGet("get/all")]
    public ActionResult<IEnumerable<PatientDto>> GetAllPatients()
    {
        var response = _executor.Dispatch(new GetPatientListQuery());
        return Ok(response.Result);
    }

    [HttpPost("add")]
    public ActionResult<Guid> AddPatient([FromBody] AddPatientCommand command)
    {
        var response = _executor.Dispatch(command);
        return Ok(response.Result);
    }

    [HttpPut("update")]
    public ActionResult<Guid> UpdatePatient([FromBody] UpdatePatientCommand command)
    {
        var response = _executor.Dispatch(command);
        return Ok(response.Result);
    }

    [HttpDelete("delete/{id}")]
    public ActionResult<Guid> DeletePatient(Guid id)
    {
        var response = _executor.Dispatch(new DeletePatientCommand(id));
        return Ok(response.Result);
    }
}