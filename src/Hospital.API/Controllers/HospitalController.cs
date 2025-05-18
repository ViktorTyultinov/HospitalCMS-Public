using Hospital.Application;
using Hospital.Application.DTOs;
using Hospital.Application.UseCases.Hospital.Commands;
using Hospital.Application.UseCases.Hospital.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HospitalController(IUseCaseExecutor executor) : ControllerBase
{
    private readonly IUseCaseExecutor _executor = executor;

    [HttpGet("get/{id}")]
    public ActionResult<HospitalDto> GetHospitalById(Guid id)
    {
        var response = _executor.Dispatch(new GetHospitalByIdQuery(id));
        return Ok(response.Result);
    }

    [HttpGet("get/all")]
    public ActionResult<IEnumerable<HospitalDto>> GetAllHospitals()
    {
        var response = _executor.Dispatch(new GetHospitalListQuery());
        return Ok(response.Result);
    }

    [HttpPost("add")]
    public ActionResult<Guid> AddHospital([FromBody] AddHospitalCommand command)
    {
        var response = _executor.Dispatch(command);
        return Ok(response.Result);
    }

    [HttpPut("update")]
    public ActionResult<Guid> UpdateHospital([FromBody] UpdateHospitalCommand command)
    {
        var response = _executor.Dispatch(command);
        return Ok(response.Result);
    }

    [HttpDelete("delete/{id}")]
    public ActionResult<Guid> DeleteHospital(Guid id)
    {
        var response = _executor.Dispatch(new DeleteHospitalCommand(id));
        return Ok(response.Result);
    }
}