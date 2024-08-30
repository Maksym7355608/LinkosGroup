namespace Light.App.Commands;

public class UpdateCommand
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<UpdateItemCommand> Items { get; set; }
}

public class UpdateItemCommand
{
    public int Id { get; set; }
    public DayOfWeek Day { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
}