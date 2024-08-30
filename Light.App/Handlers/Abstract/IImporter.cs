using Light.App.Commands;

namespace Light.App.Handlers.Abstract;

public interface IImporter
{
    Task<string> ImportDataAsync(ImportCommand cmd);
}