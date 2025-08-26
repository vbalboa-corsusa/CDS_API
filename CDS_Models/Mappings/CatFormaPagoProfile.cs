using AutoMapper;
using CDS_Models.Entities;
using CDS_Models.DTOs;

namespace CDS_Models.Mappings
{
    public class CatFormaPagoProfile : Profile
    {
        public CatFormaPagoProfile()
        {
            CreateMap<CatFormaPago, CatFormaPagoDTO>();
            CreateMap<CatFormaPagoDTO, CatFormaPago>();
        }
    }
}
