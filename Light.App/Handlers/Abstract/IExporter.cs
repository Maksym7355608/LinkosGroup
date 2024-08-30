using Light.App.Commands;
using Light.App.Dto;

namespace Light.App.Handlers.Abstract;

public interface IExporter
{
    Task<IEnumerable<ScheduleDto>> ExportAsync(ExportCommand cmd);
}