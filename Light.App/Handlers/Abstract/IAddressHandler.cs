using Light.App.Commands;
using Light.App.Dto;

namespace Light.App.Handlers.Abstract;

public interface IAddressHandler
{
    Task<AddressDto> GetAsync(string address);
    Task<AddressDto> GetAsync(int id);
    Task<List<ScheduleDto>> GetByGroupAsync(int groupId);
    Task<List<AddressDto>> SearchAsync(AddressSearchCommand cmd);
    Task<string> CheckAsync(string address);
    Task CreateAsync(AddressCreateCommand cmd);
    Task<bool> UpdateAsync(AddressUpdateCommand cmd);
    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteAsync(string address);
}