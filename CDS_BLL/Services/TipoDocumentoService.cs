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
    public class TipoDocumentoService : ITipoDocumentoService
    {
        private readonly LogistContext _context;
        private readonly IMapper _mapper;
        public TipoDocumentoService(LogistContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TipoDocsIdentDTO>> GetAllAsync()
        {
            var tiposDocumento = await _context.TiposDocumento.ToListAsync();
            return _mapper.Map<IEnumerable<TipoDocsIdentDTO>>(tiposDocumento);
        }

        public async Task<TipoDocsIdentDTO?> GetByIdAsync(int id)
        {
            var tipoDocumento = await _context.TiposDocumento.FindAsync(id);
            if (tipoDocumento == null) return null;
            return _mapper.Map<TipoDocsIdentDTO>(tipoDocumento);
        }

        public async Task<TipoDocsIdentDTO> CreateAsync(TipoDocsIdentDTO dto)
        {
            var entity = _mapper.Map<TipoDocsIdent>(dto);
            _context.TiposDocumento.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<TipoDocsIdentDTO>(entity);
        }

        public async Task<bool> UpdateAsync(int id, TipoDocsIdentDTO dto)
        {
            var entity = await _context.TiposDocumento.FindAsync(id);
            if (entity == null) return false;
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.TiposDocumento.FindAsync(id);
            if (entity == null) return false;
            _context.TiposDocumento.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
