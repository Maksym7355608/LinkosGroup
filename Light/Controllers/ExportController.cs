using Light.App.Commands;
using Light.App.Handlers.Abstract;
using Light.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Light.Controllers;

[ApiController]
[ServiceFilter(typeof(HandleExceptionAttribute))]
[Route("api/[controller]")]
public class ExportController : Controller
{
    [HttpPost("export")]
    public async Task<IActionResult> Index([FromServices] IExporter exporter, [FromBody] ExportCommand command)
    {
        var result = await exporter.ExportAsync(command);
        return Ok(result);
    }
}