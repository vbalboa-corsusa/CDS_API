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
    public class MarcaService : IMarcaService
    {
        private readonly LogistContext _context;
        private readonly IMapper _mapper;
        public MarcaService(LogistContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MarcaDTO>> GetAllAsync()
        {
            var marcas = await _context.Marcas.ToListAsync();
            return _mapper.Map<IEnumerable<MarcaDTO>>(marcas);
        }

        public async Task<MarcaDTO?> GetByIdAsync(int id)
        {
            var marca = await _context.Marcas.FindAsync(id);
            if (marca == null) return null;
            return _mapper.Map<MarcaDTO>(marca);
        }

        public async Task<MarcaDTO> CreateAsync(MarcaDTO dto)
        {
            var entity = _mapper.Map<Marca>(dto);
            _context.Marcas.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<MarcaDTO>(entity);
        }

        public async Task<bool> UpdateAsync(int id, MarcaDTO dto)
        {
            var entity = await _context.Marcas.FindAsync(id);
            if (entity == null) return false;
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Marcas.FindAsync(id);
            if (entity == null) return false;
            _context.Marcas.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
