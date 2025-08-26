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
    public class SubSubTipoNegocioService : ISubSubTipoNegocioService
    {
        private readonly LogistContext _context;
        private readonly IMapper _mapper;
        public SubSubTipoNegocioService(LogistContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SubSubTipoNegocioDTO>> GetAllAsync()
        {
            var subSubTiposNegocio = await _context.SubSubTiposNegocio.ToListAsync();
            return _mapper.Map<IEnumerable<SubSubTipoNegocioDTO>>(subSubTiposNegocio);
        }

        public async Task<SubSubTipoNegocioDTO?> GetByIdAsync(int id)
        {
            var subSubTipoNegocio = await _context.SubSubTiposNegocio.FindAsync(id);
            if (subSubTipoNegocio == null) return null;
            return _mapper.Map<SubSubTipoNegocioDTO>(subSubTipoNegocio);
        }

        public async Task<SubSubTipoNegocioDTO> CreateAsync(SubSubTipoNegocioDTO dto)
        {
            var entity = _mapper.Map<SubSubTipoNegocio>(dto);
            _context.SubSubTiposNegocio.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<SubSubTipoNegocioDTO>(entity);
        }

        public async Task<bool> UpdateAsync(int id, SubSubTipoNegocioDTO dto)
        {
            var entity = await _context.SubSubTiposNegocio.FindAsync(id);
            if (entity == null) return false;
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.SubSubTiposNegocio.FindAsync(id);
            if (entity == null) return false;
            _context.SubSubTiposNegocio.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
