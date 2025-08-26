using AutoMapper;
using CDS_Models.Entities;
using CDS_Models.DTOs;

namespace CDS_Models.Mappings
{
    public class SubTipoNegocioProfile : Profile
    {
        public SubTipoNegocioProfile()
        {
            CreateMap<SubTipoNegocio, SubTipoNegocioDTO>();
            CreateMap<SubTipoNegocioDTO, SubTipoNegocio>();
        }
    }
}
