using Light.App.Commands;
using Light.App.Handlers.Abstract;
using Light.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Light.Controllers;

[ApiController]
[ServiceFilter(typeof(HandleExceptionAttribute))]
[Route("api/[controller]")]
public class LightController : Controller
{
    private readonly ILightHandler _handler;

    public LightController(ILightHandler handler)
    {
        _handler = handler;
    }

    [HttpGet("check/{id}")]
    public async Task<IActionResult> CheckLightAsync([FromRoute] int id)
    {
        var schedule = await _handler.CheckAsync(id);
        return Ok(schedule);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute] int id)
    {
        var schedule = await _handler.GetAsync(id);
        return Ok(schedule);
    }
    
    [HttpGet("all")]
    public async Task<IActionResult> GetAllAsync()
    {
        var schedule = await _handler.GetAllAsync();
        return Ok(schedule);
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCommand command)
    { 
        await _handler.CreateAsync(command);
        return Ok();
    }
    
    [HttpPut("update")]
    public async Task<IActionResult> GetAllAsync([FromBody] UpdateCommand command)
    {
        var res = await _handler.UpdateAsync(command);
        return Ok(res);
    }
    
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> GetAllAsync([FromRoute] int id)
    {
        var res = await _handler.DeleteAsync(id);
        return Ok(res);
    }
}