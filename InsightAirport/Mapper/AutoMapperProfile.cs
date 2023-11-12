using AutoMapper;
using InsightAirport.Dtos;
using InsightAirport.Models;

namespace InsightAirport.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AirplaneModel, AirplaneDto>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name != null ? src.Name.ToUpper() : null))
                    .ForMember(dest => dest.Pilots, opt => opt.MapFrom(src => src.Pilots != null ? string.Join(", ", src.Pilots.Select(x => x.Name)) : null));

            CreateMap<PilotModel, PilotDto>();
            CreateMap<CommunicationLogModel, CommunicationLogDto>();
        }
    }
}