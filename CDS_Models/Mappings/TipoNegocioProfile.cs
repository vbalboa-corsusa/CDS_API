using AutoMapper;
using CDS_Models.Entities;
using CDS_Models.DTOs;

namespace CDS_Models.Mappings
{
    public class TipoNegocioProfile : Profile
    {
        public TipoNegocioProfile()
        {
            CreateMap<TipoNegocio, TipoNegocioDTO>();
            CreateMap<TipoNegocioDTO, TipoNegocio>();
        }
    }
}
