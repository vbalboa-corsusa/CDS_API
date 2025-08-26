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
    public class VendedorService : IVendedorService
    {
        private readonly LogistContext _context;
        private readonly IMapper _mapper;
        public VendedorService(LogistContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VendedorDTO>> GetAllAsync()
        {
            var vendedores = await _context.Vendedores.ToListAsync();
            return _mapper.Map<IEnumerable<VendedorDTO>>(vendedores);
        }

        public async Task<VendedorDTO?> GetByIdAsync(string id)
        {
            var vendedor = await _context.Vendedores.FindAsync(id);
            if (vendedor == null) return null;
            return _mapper.Map<VendedorDTO>(vendedor);
        }

        public async Task<VendedorDTO> CreateAsync(VendedorDTO dto)
        {
            // Obtiene el último IdVdr existente
            string? lastId = await _context.Vendedores
                .OrderByDescending(v => v.IdVdr)
                .Select(v => v.IdVdr)
                .FirstOrDefaultAsync();

            // Obtener el máximo IdVendedor existente (incluyendo eliminados lógicamente)
            int maxId = 0;
            if (!string.IsNullOrEmpty(lastId))
            {
                maxId = int.Parse(lastId.Substring(3)); // Extraer el número del IdVdr
            }
            var entity = _mapper.Map<Vendedor>(dto);
            entity.IdVdr = "VDR" + (maxId + 1).ToString("D7");// Para convertir Id a formato 'VDR0000001'
            _context.Vendedores.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<VendedorDTO>(entity);
        }

        public async Task<bool> UpdateAsync(string id, VendedorDTO dto)
        {
            var entity = await _context.Vendedores.FindAsync(id);
            if (entity == null) return false;
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return true;// Indica que la actualización fue exitosa
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Vendedores.FindAsync(id);
            if (entity == null) return false;
            _context.Vendedores.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
