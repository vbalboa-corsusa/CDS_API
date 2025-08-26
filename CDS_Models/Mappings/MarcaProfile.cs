using AutoMapper;
using CDS_Models.Entities;
using CDS_Models.DTOs;

namespace CDS_Models.Mappings
{
    public class MarcaProfile : Profile
    {
        public MarcaProfile()
        {
            CreateMap<Marca, MarcaDTO>();
            CreateMap<MarcaDTO, Marca>();
        }
    }
}
