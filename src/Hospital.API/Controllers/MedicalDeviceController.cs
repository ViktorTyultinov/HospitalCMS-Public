using Hospital.Application.DTOs;
using Hospital.Application.UseCases.MedicalDevice.Commands;
using Hospital.Application.UseCases.MedicalDevice.Queries;
using Microsoft.AspNetCore.Mvc;
using Hospital.Application;

namespace Hospital.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MedicalDeviceController(IUseCaseExecutor executor) : ControllerBase
{
    private readonly IUseCaseExecutor _executor = executor;

    [HttpGet("get/{id}")]
    public ActionResult<MedicalDeviceDto> GetMedicalDeviceById(Guid id)
    {
        var response = _executor.Dispatch(new GetMedicalDeviceByIdQuery(id));
        return Ok(response.Result);
    }

    [HttpGet("get/all")]
    public ActionResult<IEnumerable<MedicalDeviceDto>> GetAllMedicalDevices()
    {
        var response = _executor.Dispatch(new GetMedicalDeviceListQuery());
        return Ok(response.Result);
    }

    [HttpPost("add")]
    public ActionResult<Guid> AddMedicalDevice([FromBody] AddMedicalDeviceCommand command)
    {
        var response = _executor.Dispatch(command);
        return Ok(response.Result);
    }

    [HttpPut("update")]
    public ActionResult<Guid> UpdateMedicalDevice([FromBody] UpdateMedicalDeviceCommand command)
    {
        var response = _executor.Dispatch(command);
        return Ok(response.Result);
    }

    [HttpDelete("delete/{id}")]
    public ActionResult<Guid> DeleteMedicalDevice(Guid id)
    {
        var response = _executor.Dispatch(new DeleteMedicalDeviceCommand(id));
        return Ok(response.Result);
    }
}