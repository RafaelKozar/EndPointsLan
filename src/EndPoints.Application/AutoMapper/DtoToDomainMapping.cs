using AutoMapper;
using EndPoints.Dto;

namespace EndPoints.Application.AutoMapper
{
    public class DtoToDomainMapping : Profile
    {
        public DtoToDomainMapping()
        {
            CreateMap<EndPointGyrDto, Domain.EndPointGyr>()
                .ForMember(dest => dest.MeterModelId, opt => opt.MapFrom(src => (int)src.MeterModelId))
                .ForMember(dest => dest.SwitchState, opt => opt.MapFrom(src => (int)src.SwitchState));
        }   
    }
}
