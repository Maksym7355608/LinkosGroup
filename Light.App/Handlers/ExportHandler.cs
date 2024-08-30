using AutoMapper;
using Light.App.Commands;
using Light.App.Dto;
using Light.App.Handlers.Abstract;
using Light.Data;
using Light.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Light.App.Handlers;

public class ExportHandler : BaseHandler, IExporter
{
    private readonly IMapper _mapper;
    
    public ExportHandler(ILogger<BaseHandler> logger, SqlContext context, IMapper mapper) : base(logger, context)
    {
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<ScheduleDto>> ExportAsync(ExportCommand cmd)
    {
        IQueryable<Schedule> query = Context.Schedules;

        if (cmd.GroupIds is { Count: > 0 })
            query = query.Where(x => cmd.GroupIds.Contains(x.Id));
        if (!string.IsNullOrWhiteSpace(cmd.Address))
            query = query.Where(x => x.Addresses.Any(a =>
                EF.Functions.Like(a.AddressName, $"%{cmd.Address}%")));

        var schedules = await query.Include(x => x.Items).ToListAsync();

        return _mapper.Map<IEnumerable<ScheduleDto>>(schedules);
    }
}