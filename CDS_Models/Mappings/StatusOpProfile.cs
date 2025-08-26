using AutoMapper;
using CDS_Models.Entities;
using CDS_Models.DTOs;

namespace CDS_Models.Mappings
{
    public class StatusOpProfile : Profile
    {
        public StatusOpProfile()
        {
            CreateMap<EstadosOp, EstadosOpDTO>();
            CreateMap<EstadosOpDTO, EstadosOp>();
        }
    }
}
