namespace Light.Data.Entities;

public class ScheduleItem
{
    public int Id { get; set; }
    /// <summary>
    /// enum - DayOfWeek
    /// </summary>
    public int Day { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    
    
    public int ScheduleId { get; set; }
    public Schedule Schedule { get; set; }
}