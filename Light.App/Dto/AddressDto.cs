namespace Light.App.Dto;

public class AddressDto : AddressShortDto
{
    public ScheduleWithItemsDto? Schedule { get; set; }
}

public class AddressShortDto
{
    public int Id { get; set; }
    public string AddressName { get; set; }
    
    public int? ScheduleId { get; set; }
}