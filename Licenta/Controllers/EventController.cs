using Licenta.Services.DTOs.Event;
using Licenta.Services.Interfaces;
using Licenta.Services.QueryParameters;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Licenta.Api.Controllers;

[Route("api/events")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly IEventManager _eventManager;
    public EventController(IEventManager eventManager)
    {
        _eventManager = eventManager;
    }

    [HttpPost]
    public async Task<IActionResult> AddEvent([FromBody] EventPostDTO eventDto)
    {
        var result = await _eventManager.AddAsync(eventDto);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> ListEvents([FromQuery] EventParameters parameters)
    {
        var result = await _eventManager.ListEventsAsync(parameters);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEventProfile([FromRoute] long id)
    {
        var result = await _eventManager.GetEventProfileByIdAsync(id);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] EventPutDTO eventDto)
    {
        var result = await _eventManager.UpdateAsync(eventDto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent([FromRoute] long id)
    {
        await _eventManager.DeleteAsync(id);
        return Ok("Event DELETED successfully");
    }
}
