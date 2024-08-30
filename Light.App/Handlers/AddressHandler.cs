using AutoMapper;
using Light.App.Commands;
using Light.App.Dto;
using Light.App.Handlers.Abstract;
using Light.Data;
using Light.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Light.App.Handlers;

public class AddressHandler: BaseHandler, IAddressHandler
{
    private readonly IMapper _mapper;
    private readonly ILightHandler _lightHandler;
    
    public AddressHandler(ILogger<BaseHandler> logger, SqlContext context, IMapper mapper,
        ILightHandler lightHandler) 
        : base(logger, context)
    {
        _mapper = mapper;
        _lightHandler = lightHandler;
    }

    public async Task<AddressDto> GetAsync(string address)
    {
        return _mapper.Map<AddressDto>(await Context.Addresses.Where(x =>
                EF.Functions.Like(x.AddressName, $"%{address}%"))
            .Include(x => x.Schedule)
            .ThenInclude(x => x.Items)
            .FirstOrDefaultAsync());
    }

    public async Task<AddressDto> GetAsync(int id)
    {
        return _mapper.Map<AddressDto>(await Context.Addresses.Where(x => x.Id == id)
            .Include(x => x.Schedule)
            .ThenInclude(x => x.Items)
            .FirstOrDefaultAsync());
    }

    public async Task<List<ScheduleDto>> GetByGroupAsync(int groupId)
    {
        return _mapper.Map<List<ScheduleDto>>(await Context.Schedules.Where(x => x.Id == groupId)
            .Include(x => x.Addresses)
            .Include(x => x.Items)
            .ToListAsync());
    }

    public async Task<List<AddressDto>> SearchAsync(AddressSearchCommand cmd)
    {
        IQueryable<Address> query = Context.Addresses;
        if (cmd.Id.HasValue)
            query = query.Where(x => x.Id == cmd.Id.Value);
        if (!string.IsNullOrWhiteSpace(cmd.Address))
            query = query.Where(x => EF.Functions.Like(x.AddressName, $"%{cmd.Address}%"));
        if (cmd.Date.HasValue)
            query = query.Where(x => x.Schedule.Items.Any(item => (DayOfWeek)item.Day == cmd.Date.Value.DayOfWeek));

        var addresses = await query
            .Include(x => x.Schedule)
            .ThenInclude(x => x.Items)
            .ToListAsync();
        
        return _mapper.Map<List<AddressDto>>(addresses);
    }

    public async Task<string> CheckAsync(string address)
    {
        var dto = await GetAsync(address);
        
        if(dto == null || !dto.ScheduleId.HasValue)
            return "We don't have information about this group";
        
        return await _lightHandler.CheckAsync(dto.ScheduleId.Value);
    }

    public async Task CreateAsync(AddressCreateCommand cmd)
    {
        var existedAddress = await Context.Addresses
            .Where(x => x.AddressName == cmd.AddressName)
            .FirstOrDefaultAsync();
        if (existedAddress != null)
        {
            Logger.LogWarning("You have already existed schedule groups: {0}", string.Join(", ", existedAddress));
            throw new ArgumentException("Incorrect group number");
        }
        await Context.Addresses.AddAsync(_mapper.Map<Address>(cmd));
        await Context.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(AddressUpdateCommand cmd)
    {
        var address = _mapper.Map<Address>(cmd);
        var res = Context.Addresses.Update(address);
        await Context.SaveChangesAsync();
        return res.State == EntityState.Modified;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var result = await GetAsync(id);
        var deleted = Context.Addresses.Remove(_mapper.Map<Address>(result));
        await Context.SaveChangesAsync();
        return deleted.State == EntityState.Deleted;
    }

    public async Task<bool> DeleteAsync(string address)
    {
        var result = await GetAsync(address);
        var deleted = Context.Addresses.Remove(_mapper.Map<Address>(result));
        await Context.SaveChangesAsync();
        return deleted.State == EntityState.Deleted;
    }
}