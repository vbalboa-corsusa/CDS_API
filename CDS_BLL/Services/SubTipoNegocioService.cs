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
    public class SubTipoNegocioService : ISubTipoNegocioService
    {
        private readonly LogistContext _context;
        private readonly IMapper _mapper;
        public SubTipoNegocioService(LogistContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SubTipoNegocioDTO>> GetAllAsync()
        {
            var subTiposNegocio = await _context.SubTiposNegocio.ToListAsync();
            return _mapper.Map<IEnumerable<SubTipoNegocioDTO>>(subTiposNegocio);
        }

        public async Task<SubTipoNegocioDTO?> GetByIdAsync(int id)
        {
            var subTipoNegocio = await _context.SubTiposNegocio.FindAsync(id);
            if (subTipoNegocio == null) return null;
            return _mapper.Map<SubTipoNegocioDTO>(subTipoNegocio);
        }

        public async Task<SubTipoNegocioDTO> CreateAsync(SubTipoNegocioDTO dto)
        {
            var entity = _mapper.Map<SubTipoNegocio>(dto);
            _context.SubTiposNegocio.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<SubTipoNegocioDTO>(entity);
        }

        public async Task<bool> UpdateAsync(int id, SubTipoNegocioDTO dto)
        {
            var entity = await _context.SubTiposNegocio.FindAsync(id);
            if (entity == null) return false;
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.SubTiposNegocio.FindAsync(id);
            if (entity == null) return false;
            _context.SubTiposNegocio.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
