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
                    IdVendedor = v.IdVendedor,
                    IdTdi = v.IdTdi,
                    NumDocVendedor = v.NumDocVendedor,
                    NombreVendedor = v.NombreVendedor,
                    IbLider = v.IbLider,
                    Estado = v.Estado
                })
                .ToListAsync();
        }

        public async Task<VendedorDTO?> GetByIdAsync(int id)
        {
            var v = await _context.Vendedores.FindAsync(id);
            if (v == null) return null;
            return new VendedorDTO
            {
                IdVendedor = v.IdVendedor,
                IdTdi = v.IdTdi,
                NumDocVendedor = v.NumDocVendedor,
                NombreVendedor = v.NombreVendedor,
                IbLider = v.IbLider,
                Estado = v.Estado
            };
        }

        public async Task<VendedorDTO> CreateAsync(VendedorDTO dto)
        {
            var entity = new Vendedor
            {
                IdTdi = dto.IdTdi,
                NumDocVendedor = dto.NumDocVendedor,
                NombreVendedor = dto.NombreVendedor,
                IbLider = dto.IbLider,
                Estado = dto.Estado
            };
            _context.Vendedores.Add(entity);
            await _context.SaveChangesAsync();
            dto.IdVendedor = entity.IdVendedor;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, VendedorDTO dto)
        {
            var entity = await _context.Vendedores.FindAsync(id);
            if (entity == null) return false;
            entity.IdTdi = dto.IdTdi;
            entity.NumDocVendedor = dto.NumDocVendedor;
            entity.NombreVendedor = dto.NombreVendedor;
            entity.IbLider = dto.IbLider;
            entity.Estado = dto.Estado;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Vendedores.FindAsync(id);
            if (entity == null) return false;
            _context.Vendedores.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
} 