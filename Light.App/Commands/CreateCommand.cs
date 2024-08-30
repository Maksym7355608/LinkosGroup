namespace Light.App.Commands;

public class CreateCommand
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<CreateItemCommand> Items { get; set; }
}

public class CreateItemCommand
{
    public DayOfWeek Day { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
}