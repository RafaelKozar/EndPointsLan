using AutoMapper;
using EndPoints.Dto;

namespace EndPoints.Application.AutoMapper
{
    public class DomainToDtoMapping : Profile
    {
        public DomainToDtoMapping()
        {
            CreateMap<Domain.EndPointGyr, EndPointGyrDto>();                
        }
    }
}
