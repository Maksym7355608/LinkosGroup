using Microsoft.AspNetCore.Http;

namespace Light.App.Commands;

public class ImportCommand
{
    public DayOfWeek Day { get; set; }
    public IFormFile File { get; set; }
}