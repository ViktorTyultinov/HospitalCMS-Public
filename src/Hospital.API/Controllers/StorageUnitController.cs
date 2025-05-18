using Hospital.Application.DTOs;
using Hospital.Application.UseCases.StorageUnit.Commands;
using Hospital.Application.UseCases.StorageUnit.Queries;
using Microsoft.AspNetCore.Mvc;
using Hospital.Application;

namespace Hospital.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StorageUnitController(IUseCaseExecutor executor) : ControllerBase
{
    private readonly IUseCaseExecutor _executor = executor;

    [HttpGet("get/{id}")]
    public ActionResult<StorageUnitDto> GetStorageUnitById(Guid id)
    {
        var response = _executor.Dispatch(new GetStorageUnitByIdQuery(id));
        return Ok(response.Result);
    }

    [HttpGet("get/all")]
    public ActionResult<IEnumerable<StorageUnitDto>> GetAllStorageUnits()
    {
        var response = _executor.Dispatch(new GetStorageUnitListQuery());
        return Ok(response.Result);
    }

    [HttpPost("add")]
    public ActionResult<Guid> AddStorageUnit([FromBody] AddStorageUnitCommand command)
    {
        var response = _executor.Dispatch(command);
        return Ok(response.Result);
    }

    [HttpPut("update")]
    public ActionResult<Guid> UpdateStorageUnit([FromBody] UpdateStorageUnitCommand command)
    {
        var response = _executor.Dispatch(command);
        return Ok(response.Result);
    }

    [HttpDelete("delete/{id}")]
    public ActionResult<Guid> DeleteStorageUnit(Guid id)
    {
        var response = _executor.Dispatch(new DeleteStorageUnitCommand(id));
        return Ok(response.Result);
    }
}