using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDS_BLL.Interfaces;
using CDS_Models.DTOs;
using CDS_Models;
using CDS_DAL;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CDS_Models.Entities;

namespace CDS_BLL.Services
{
    public class TipoNegocioService : ITipoNegocioService
    {
        private readonly LogistContext _context;
        private readonly IMapper _mapper;
        public TipoNegocioService(LogistContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TipoNegocioDTO>> GetAllAsync()
        {
            var tiposNegocio = await _context.TipoNegocio.ToListAsync();
            return _mapper.Map<IEnumerable<TipoNegocioDTO>>(tiposNegocio);
        }

        public async Task<TipoNegocioDTO?> GetByIdAsync(int id)
        {
            var tipoNegocio = await _context.TipoNegocio.FindAsync(id);
            if (tipoNegocio == null) return null;
            return _mapper.Map<TipoNegocioDTO>(tipoNegocio);
        }

        public async Task<TipoNegocioDTO> CreateAsync(TipoNegocioDTO dto)
        {
            var entity = _mapper.Map<TipoNegocio>(dto);
            _context.TipoNegocio.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<TipoNegocioDTO>(entity);
        }

        public async Task<bool> UpdateAsync(int id, TipoNegocioDTO dto)
        {
            var entity = await _context.TipoNegocio.FindAsync(id);
            if (entity == null) return false;
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.TipoNegocio.FindAsync(id);
            if (entity == null) return false;
            _context.TipoNegocio.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
