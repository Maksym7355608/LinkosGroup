using Light.App.Commands;
using Light.App.Handlers.Abstract;
using Light.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Light.Controllers;

[ApiController]
[ServiceFilter(typeof(HandleExceptionAttribute))]
[Route("api/[controller]")]
public class AddressController : Controller
{
    private readonly IAddressHandler _handler;

    public AddressController(IAddressHandler handler)
    {
        _handler = handler;
    }

    [HttpGet("check/{id}")]
    public async Task<IActionResult> CheckLightAsync([FromRoute] string address)
    {
        var schedule = await _handler.CheckAsync(address);
        return Ok(schedule);
    }

    [HttpGet("by-address/{id}")]
    public async Task<IActionResult> GetByAddressAsync([FromRoute] string address)
    {
        var schedule = await _handler.GetAsync(address);
        return Ok(schedule);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute] int id)
    {
        var schedule = await _handler.GetAsync(id);
        return Ok(schedule);
    }
    
    [HttpGet("all/{groupId}")]
    public async Task<IActionResult> GetAllAsync([FromRoute] int groupId)
    {
        var schedule = await _handler.GetByGroupAsync(groupId);
        return Ok(schedule);
    }
    
    [HttpPost("search")]
    public async Task<IActionResult> SearchAsync([FromBody] AddressSearchCommand command)
    { 
        var addresses = await _handler.SearchAsync(command);
        return Ok(addresses);
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync([FromBody] AddressCreateCommand command)
    { 
        await _handler.CreateAsync(command);
        return Ok();
    }
    
    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync([FromBody] AddressUpdateCommand command)
    {
        var res = await _handler.UpdateAsync(command);
        return Ok(res);
    }
    
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        var res = await _handler.DeleteAsync(id);
        return Ok(res);
    }
    
    [HttpDelete("delete/by-address/{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] string id)
    {
        var res = await _handler.DeleteAsync(id);
        return Ok(res);
    }
}