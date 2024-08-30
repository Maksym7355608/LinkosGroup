namespace Light.Data.Entities;

public class Schedule
{
    /// <summary>
    /// GroupId
    /// </summary>
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<ScheduleItem> Items { get; set; }
    public List<Address> Addresses { get; set; }
}