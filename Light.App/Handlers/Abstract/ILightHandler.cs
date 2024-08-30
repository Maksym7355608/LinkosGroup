using Light.App.Commands;
using Light.App.Dto;

namespace Light.App.Handlers.Abstract;

public interface ILightHandler
{
    Task<string> CheckAsync(int id);
    Task<ScheduleWithItemsDto> GetAsync(int id);
    Task<IEnumerable<ScheduleWithItemsDto>> GetAllAsync();
    Task CreateAsync(CreateCommand cmd);
    Task<bool> UpdateAsync(UpdateCommand cmd);
    Task<bool> DeleteAsync(int id);
}