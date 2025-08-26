using AutoMapper;
using CDS_Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDS_Models.Mappings
{
    public class ProyectoProfile : Profile
    {
        public ProyectoProfile()
        {
            CreateMap<Proyecto, ProyectoDTO>().ReverseMap();
        }
    }
}
