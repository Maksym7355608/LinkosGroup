namespace Light.Data.Entities;

public class Address
{
    public int Id { get; set; }
    public string AddressName { get; set; }
    
    public int? ScheduleId { get; set; }
    public Schedule Schedule { get; set; }
}