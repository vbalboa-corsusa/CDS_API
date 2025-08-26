using AutoMapper;
using CDS_Models.Entities;
using CDS_Models.DTOs;

namespace CDS_Models.Mappings
{
    public class VendedorProfile : Profile
    {
        public VendedorProfile()
        {
            CreateMap<Vendedor, VendedorDTO>();
            CreateMap<VendedorDTO, Vendedor>();
        }
    }
}
