namespace Light.App.Dto;

public class ScheduleDto : ScheduleWithItemsDto
{
    public List<AddressShortDto> Addresses { get; set; }
}

public class ScheduleWithItemsDto : ScheduleShortDto
{
    public List<ScheduleItemDto> Items { get; set; }
}

public class ScheduleShortDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}