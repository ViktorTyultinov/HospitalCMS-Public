using Hospital.Application.DTOs;
using Hospital.Application.UseCases.Room.Commands;
using Hospital.Application.UseCases.Room.Queries;
using Microsoft.AspNetCore.Mvc;
using Hospital.Application;

namespace Hospital.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoomController(IUseCaseExecutor executor) : ControllerBase
{
    private readonly IUseCaseExecutor _executor = executor;

    [HttpGet("get/{id}")]
    public ActionResult<RoomDto> GetRoomById(Guid id)
    {
        var response = _executor.Dispatch(new GetRoomByIdQuery(id));
        return Ok(response.Result);
    }

    [HttpGet("get/all")]
    public ActionResult<IEnumerable<RoomDto>> GetAllRooms()
    {
        var response = _executor.Dispatch(new GetRoomListQuery());
        return Ok(response.Result);
    }

    [HttpPost("add")]
    public ActionResult<Guid> AddRoom([FromBody] AddRoomCommand command)
    {
        var response = _executor.Dispatch(command);
        return Ok(response.Result);
    }

    [HttpPut("update")]
    public ActionResult<Guid> UpdateRoom([FromBody] UpdateRoomCommand command)
    {
        var response = _executor.Dispatch(command);
        return Ok(response.Result);
    }

    [HttpDelete("delete/{id}")]
    public ActionResult<Guid> DeleteRoom(Guid id)
    {
        var response = _executor.Dispatch(new DeleteRoomCommand(id));
        return Ok(response.Result);
    }
}