using AutoMapper;
using CDS_Models.Entities;
using CDS_Models.DTOs;

namespace CDS_Models.Mappings
{
    public class MonedaProfile : Profile
    {
        public MonedaProfile()
        {
            CreateMap<Moneda, MonedaDTO>();
            CreateMap<MonedaDTO, Moneda>();
        }
    }
}
