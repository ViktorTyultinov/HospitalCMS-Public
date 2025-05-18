using Hospital.Application.DTOs;
using Hospital.Application.UseCases.Prescription.Commands;
using Hospital.Application.UseCases.Prescription.Queries;
using Microsoft.AspNetCore.Mvc;
using Hospital.Application;

namespace Hospital.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PrescriptionController(IUseCaseExecutor executor) : ControllerBase
{
    private readonly IUseCaseExecutor _executor = executor;

    [HttpGet("get/{id}")]
    public ActionResult<PrescriptionDto> GetPrescriptionById(Guid id)
    {
        var response = _executor.Dispatch(new GetPrescriptionByIdQuery(id));
        return Ok(response.Result);
    }

    [HttpGet("get/all")]
    public ActionResult<IEnumerable<PrescriptionDto>> GetAllPrescriptions()
    {
        var response = _executor.Dispatch(new GetPrescriptionListQuery());
        return Ok(response.Result);
    }

    [HttpPost("add")]
    public ActionResult<Guid> AddPrescription([FromBody] AddPrescriptionCommand command)
    {
        var response = _executor.Dispatch(command);
        return Ok(response.Result);
    }

    [HttpPut("update")]
    public ActionResult<Guid> UpdatePrescription([FromBody] UpdatePrescriptionCommand command)
    {
        var response = _executor.Dispatch(command);
        return Ok(response.Result);
    }

    [HttpDelete("delete/{id}")]
    public ActionResult<Guid> DeletePrescription(Guid id)
    {
        var response = _executor.Dispatch(new DeletePrescriptionCommand(id));
        return Ok(response.Result);
    }
}