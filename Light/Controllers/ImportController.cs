using Light.App.Commands;
using Light.App.Handlers.Abstract;
using Light.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Light.Controllers;

[ApiController]
[ServiceFilter(typeof(HandleExceptionAttribute))]
[Route("api/[controller]")]
public class ImportController : Controller
{
    [HttpPost("upload")]
    public async Task<IActionResult> ImportAsync([FromServices] IImporter importer, [FromForm] ImportCommand command)
    {
        var result = await importer.ImportDataAsync(command);
        return Ok(result);
    }
}