using AutoMapper;
using CDS_Models.Entities;
using CDS_Models.DTOs;

namespace CDS_Models.Mappings
{
    public class SubSubTipoNegocioProfile : Profile
    {
        public SubSubTipoNegocioProfile()
        {
            CreateMap<SubSubTipoNegocio, SubSubTipoNegocioDTO>();
            CreateMap<SubSubTipoNegocioDTO, SubSubTipoNegocio>();
        }
    }
}
