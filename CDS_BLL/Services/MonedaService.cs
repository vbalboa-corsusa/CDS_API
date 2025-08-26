using System.Collections.Generic;
using System.Threading.Tasks;
using CDS_BLL.Interfaces;
using CDS_Models.DTOs;
using CDS_Models.Entities;
using AutoMapper;
using CDS_DAL;
using Microsoft.EntityFrameworkCore;

namespace CDS_BLL.Services
{
    public class MonedaService : IMonedaService
    {
        private readonly LogistContext _context;
        private readonly IMapper _mapper;

        public MonedaService(LogistContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MonedaDTO>> GetAllAsync()
        {
            var monedas = await _context.Monedas.ToListAsync();
            return _mapper.Map<IEnumerable<MonedaDTO>>(monedas);
        }
    }
}
