using Hospital.Application.DTOs;
using Hospital.Application.UseCases.Bed.Commands;
using Hospital.Application.UseCases.Bed.Queries;
using Microsoft.AspNetCore.Mvc;
using Hospital.Application;

namespace Hospital.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BedController(IUseCaseExecutor executor) : ControllerBase
{
    private readonly IUseCaseExecutor _executor = executor;

    [HttpGet("get/{id}")]
    public ActionResult<BedDto> GetBedById(Guid id)
    {
        var response = _executor.Dispatch(new GetBedByIdQuery(id));
        return Ok(response.Result);
    }

    [HttpGet("get/all")]
    public ActionResult<IEnumerable<BedDto>> GetAllBeds()
    {
        var response = _executor.Dispatch(new GetBedListQuery());
        return Ok(response.Result);
    }

    [HttpPost("add")]
    public ActionResult<Guid> AddBed([FromBody] AddBedCommand command)
    {
        var response = _executor.Dispatch(command);
        return Ok(response.Result);
    }

    [HttpPut("update")]
    public ActionResult<Guid> UpdateBed([FromBody] UpdateBedCommand command)
    {
        var response = _executor.Dispatch(command);
        return Ok(response.Result);
    }

    [HttpDelete("delete/{id}")]
    public ActionResult<Guid> DeleteBed(Guid id)
    {
        var response = _executor.Dispatch(new DeleteBedCommand(id));
        return Ok(response.Result);
    }
}