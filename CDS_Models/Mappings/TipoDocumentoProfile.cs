using AutoMapper;
using CDS_Models.Entities;
using CDS_Models.DTOs;

namespace CDS_Models.Mappings
{
    public class TipoDocumentoProfile : Profile
    {
        public TipoDocumentoProfile()
        {
            CreateMap<TipoDocsIdent, TipoDocsIdentDTO>();
            CreateMap<TipoDocsIdentDTO, TipoDocsIdent>();
        }
    }
}
