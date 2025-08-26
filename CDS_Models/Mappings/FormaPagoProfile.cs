using AutoMapper;
using CDS_Models.Entities;
using CDS_Models.DTOs;

namespace CDS_Models.Mappings
{
    public class FormaPagoProfile : Profile
    {
        public FormaPagoProfile()
        {
            CreateMap<FormaPago, FormaPagoDTO>();
            CreateMap<FormaPagoDTO, FormaPago>();
        }
    }
}
