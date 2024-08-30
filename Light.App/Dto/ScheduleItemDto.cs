namespace Light.App.Dto;

public class ScheduleItemDto
{
    public int Id { get; set; }
    public DayOfWeek Day { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public int ScheduleId { get; set; }
}