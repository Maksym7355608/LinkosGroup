using AutoMapper;
using Light.App.Commands;
using Light.App.Dto;
using Light.App.Handlers.Abstract;
using Light.Data;
using Light.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Light.App.Handlers;

public class LightHandler : BaseHandler, ILightHandler
{
    private readonly IMapper _mapper;
    
    public LightHandler(ILogger<BaseHandler> logger, SqlContext context, IMapper mapper) : base(logger, context)
    {
        _mapper = mapper;
    }

    public async Task<ScheduleWithItemsDto> GetAsync(int id)
    {
        return _mapper.Map<ScheduleWithItemsDto>(await Context.Schedules
            .Include(x => x.Items)
            .FirstOrDefaultAsync(s => s.Id == id));
    }

    public async Task<string> CheckAsync(int id)
    {
        var schedule = await GetAsync(id);
        if (schedule == null)
            return "We don't have information about this group";
        var time = schedule.Items.FirstOrDefault(x =>
            x.StartTime.ToTimeSpan() < DateTime.Now.TimeOfDay && x.EndTime.ToTimeSpan() >= DateTime.Now.TimeOfDay);
        if (time != null)
            return
                $"You don't have light now. Light will turn on in {(int)(time.EndTime.ToTimeSpan() - DateTime.Now.TimeOfDay).TotalMinutes} minutes";

        return "You have light";
    }

    public async Task<IEnumerable<ScheduleWithItemsDto>> GetAllAsync()
    {
        return _mapper.Map<IEnumerable<ScheduleWithItemsDto>>(await Context.Schedules
            .Include(x => x.Items)
            .ToArrayAsync());
    }

    public async Task CreateAsync(CreateCommand cmd)
    {
        var existedSchedules = await Context.Schedules
            .Where(x => cmd.Id == x.Id)
            .FirstOrDefaultAsync();
        if (existedSchedules != null)
        {
            Logger.LogWarning("You have already existed schedule groups: {0}", string.Join(", ", existedSchedules));
            throw new ArgumentException("Incorrect group number");
        }
            
        await Context.Schedules.AddAsync(_mapper.Map<Schedule>(cmd));
        await Context.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(UpdateCommand cmd)
    {
        var existedSchedules = await Context.Schedules
            .Where(x => cmd.Id == x.Id)
            .Include(x => x.Items)
            .FirstOrDefaultAsync();
        if (existedSchedules == null)
        {
            Logger.LogWarning("You have not existed schedule group");
            return false;
        }
        Context.ScheduleItems.RemoveRange(existedSchedules.Items);
        var res = Context.Schedules.Update(_mapper.Map(cmd, existedSchedules));
        await Context.SaveChangesAsync();
        return res.State == EntityState.Modified;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existedSchedules = await Context.Schedules
            .Where(x => id == x.Id)
            .Include(x => x.Items)
            .FirstOrDefaultAsync();
        if (existedSchedules == null)
        {
            Logger.LogWarning("You have not existed schedule group");
            return false;
        }
        var result = Context.Schedules.Remove(existedSchedules);
        await Context.SaveChangesAsync();
        return result.State == EntityState.Deleted;
    }
}