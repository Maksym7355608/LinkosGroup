using System.Security.Cryptography;
using System.Text;
using Light.App.Commands;
using Light.App.Handlers.Abstract;
using Light.Data;
using Light.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Light.App.Handlers;

public class ImportHandler : BaseHandler, IImporter
{
    public ImportHandler(ILogger<BaseHandler> logger, SqlContext context) : base(logger, context)
    {
    }
    
    public async Task<string> ImportDataAsync(ImportCommand cmd)
    {
        try
        {
            string message = null;
            var stream = cmd.File.OpenReadStream();
            var readingResult = await ReadFileAsync(stream, cmd.Day);
            var existedSchedules = await Context.Schedules
                .Where(x => readingResult.Select(r => r.Id).Contains(x.Id))
                .Include(x => x.Items)
                .ToListAsync();
            if (existedSchedules.Count > 0)
            {
                Logger.LogWarning("You have already existed schedule groups: {0}. Old data has been overwritted", string.Join(", ", existedSchedules));
                message = $"You have already existed schedule groups: {string.Join(", ", existedSchedules.Select(x => x.Id))}. Old data has been overwritted";
                Context.Schedules.RemoveRange(existedSchedules);
            }
            
            await Context.Schedules.AddRangeAsync(readingResult);
            await Context.SaveChangesAsync();
            return message;
        }
        catch (ArgumentException e)
        {
            Logger.LogError(e.Message);
            return e.Message;
        }
        catch (Exception)
        {
            Logger.LogError("An unexpected error occurred");
            return "An unexpected error occurred";
        }
    }

    private async Task<List<Schedule>> ReadFileAsync(Stream stream, DayOfWeek day)
    {
        var schedules = new List<Schedule>();

        using var reader = new StreamReader(stream, Encoding.UTF8);
        var line = string.Empty;
        while ((line = await reader.ReadLineAsync()) != null)
        {
            var stages = line.Trim().Split('.');
            if (stages.Length != 2 || !int.TryParse(stages[0], out var groupId))
                throw new ArgumentException("Incorrect input file");
            var values = stages[1].Trim().Split(';');

            var schedule = new Schedule()
            {
                Id = groupId,
                Items = []
            };

            foreach (var item in values)
            {
                var scheduleItemStr = item.Trim().Split('-');
                if (scheduleItemStr.Length != 2) throw new ArgumentException("Incorrect time");

                var scheduleItem = new ScheduleItem()
                {
                    ScheduleId = groupId,
                    Day = (int)day,
                    StartTime = ParseValue(scheduleItemStr[0]),
                    EndTime = ParseValue(scheduleItemStr[1]),
                };
                
                schedule.Items.Add(scheduleItem);
            }
            schedules.Add(schedule);
        }

        return schedules;
    }

    private TimeOnly ParseValue(string value)
    {
        if (!TimeOnly.TryParse(value, out var result))
            throw new ArgumentException($"Value {value} have incorrect format");
        return result;
    }
}