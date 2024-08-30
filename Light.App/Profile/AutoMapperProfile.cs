using Light.App.Commands;
using Light.App.Dto;
using Light.Data.Entities;

namespace Light.App.Profile;

public class AutoMapperProfile : AutoMapper.Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Schedule, ScheduleDto>().ReverseMap();
        CreateMap<ScheduleItem, ScheduleItemDto>().ReverseMap();
        CreateMap<Schedule, ScheduleWithItemsDto>().ReverseMap();
        CreateMap<ScheduleItem, ScheduleShortDto>().ReverseMap();

        CreateMap<CreateCommand, Schedule>();
        CreateMap<UpdateCommand, Schedule>();
        CreateMap<CreateItemCommand, ScheduleItem>()
            .ForMember(x => x.StartTime, dst =>
            dst.MapFrom(s => TimeOnly.Parse(s.StartTime)))
            .ForMember(x => x.EndTime, dst =>
            dst.MapFrom(s => TimeOnly.Parse(s.EndTime)));
        CreateMap<UpdateItemCommand, ScheduleItem>()
            .ForMember(x => x.StartTime, dst =>
                dst.MapFrom(s => TimeOnly.Parse(s.StartTime)))
            .ForMember(x => x.EndTime, dst =>
                dst.MapFrom(s => TimeOnly.Parse(s.EndTime)));

        CreateMap<Address, AddressDto>().ReverseMap();
        CreateMap<Address, AddressShortDto>().ReverseMap();
        CreateMap<AddressCreateCommand, Address>();
        CreateMap<AddressUpdateCommand, Address>();
    }
}