using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDS_BLL.Interfaces;
using CDS_Models.DTOs;
using CDS_Models;
using CDS_DAL;
using Microsoft.EntityFrameworkCore;

namespace CDS_BLL.Services
{
    public class VendedorService : IVendedorService
    {
        private readonly LogistContext _context;
        public VendedorService(LogistContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VendedorDTO>> GetAllAsync()
        {
            return await _context.Vendedores
                .Select(v => new VendedorDTO
                {
                    IdVdr = v.IdVdr,
                    IdTdi = v.IdTdi,
                    NDoc = v.NDoc,
                    NomVdr = v.NomVdr,
                    IbLider = v.IbLider,
                    Estado = v.Estado
                })
                .ToListAsync();
        }

        public async Task<VendedorDTO?> GetByIdAsync(string id)
        {
            var v = await _context.Vendedores.FindAsync(id);
            if (v == null) return null;
            return new VendedorDTO
            {
                IdVdr = v.IdVdr,
                IdTdi = v.IdTdi,
                NDoc = v.NDoc,
                NomVdr = v.NomVdr,
                IbLider = v.IbLider,
                Estado = v.Estado
            };
        }

        public async Task<VendedorDTO> CreateAsync(VendedorDTO dto)
        {
            // Obtener el máximo IdVendedor existente (incluyendo eliminados lógicamente)
            int maxId = 0;
            if (await _context.Vendedores.AnyAsync())
            {
                maxId = await _context.Vendedores
                    .Select(v => int.Parse(v.IdVdr.Substring(3))) // Extraer el número del IdVdr
                    .MaxAsync();
            }
            var entity = new Vendedor
            {
                IdVdr = "VDR" + (maxId + 1).ToString("D7"),// Para convertir Id a formato 'VDR0000001'
                IdTdi = dto.IdTdi,
                NDoc = dto.NDoc,
                NomVdr = dto.NomVdr,
                IbLider = dto.IbLider,
                Estado = dto.Estado
            };
            _context.Vendedores.Add(entity);
            await _context.SaveChangesAsync();
            dto.IdVdr = entity.IdVdr;
            return dto;
        }

        public async Task<bool> UpdateAsync(string id, VendedorDTO dto)
        {
            var entity = await _context.Vendedores.FindAsync(id);
            if (entity == null) return false;
            entity.IdTdi = dto.IdTdi;
            entity.NDoc = dto.NDoc;
            entity.NomVdr = dto.NomVdr;
            entity.IbLider = dto.IbLider;
            entity.Estado = dto.Estado;
            await _context.SaveChangesAsync();
            return true;
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