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
    public class ProductoService : IProductoService
    {
        private readonly LogistContext _context;
        public ProductoService(LogistContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductoDTO>> GetAllAsync()
        {
            return await _context.Productos
                .Select(p => new ProductoDTO
                {
                    IdProd = p.IdProd,
                    IdMarca = p.IdMarca,
                    CodComercial1 = p.CodComercial1,
                    CodComercial2 = p.CodComercial2,
                    CodComercial3 = p.CodComercial3,
                    Descripcion = p.Descripcion,
                    NomCorto = p.NomCorto,
                    IdClase = p.IdClase,
                    IdSClase = p.IdSClase,
                    IdSSClase = p.IdSSClase,
                    IdCc = p.IdCc,
                    IdScc = p.IdScc,
                    IdSscc = p.IdSscc,
                    Estado = p.Estado
                })
                .ToListAsync();
        }

        public async Task<ProductoDTO?> GetByIdAsync(int id)
        {
            var p = await _context.Productos.FindAsync(id);
            if (p == null) return null;
            return new ProductoDTO
            {
                IdProd = p.IdProd,
                IdMarca = p.IdMarca,
                CodComercial1 = p.CodComercial1,
                CodComercial2 = p.CodComercial2,
                CodComercial3 = p.CodComercial3,
                Descripcion = p.Descripcion,
                NomCorto = p.NomCorto,
                IdClase = p.IdClase,
                IdSClase = p.IdSClase,
                IdSSClase = p.IdSSClase,
                IdCc = p.IdCc,
                IdScc = p.IdScc,
                IdSscc = p.IdSscc,
                Estado = p.Estado
            };
        }

        public async Task<ProductoDTO> CreateAsync(ProductoDTO dto)
        {
            var entity = new Producto
            {
                IdMarca = dto.IdMarca,
                CodComercial1 = dto.CodComercial1,
                CodComercial2 = dto.CodComercial2,
                CodComercial3 = dto.CodComercial3,
                Descripcion = dto.Descripcion,
                NomCorto = dto.NomCorto,
                IdClase = dto.IdClase,
                IdSClase = dto.IdSClase,
                IdSSClase = dto.IdSSClase,
                IdCc = dto.IdCc,
                IdScc = dto.IdScc,
                IdSscc = dto.IdSscc,
                Estado = dto.Estado
            };
            _context.Productos.Add(entity);
            await _context.SaveChangesAsync();
            dto.IdProd = entity.IdProd;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, ProductoDTO dto)
        {
            var entity = await _context.Productos.FindAsync(id);
            if (entity == null) return false;
            entity.IdMarca = dto.IdMarca;
            entity.CodComercial1 = dto.CodComercial1;
            entity.CodComercial2 = dto.CodComercial2;
            entity.CodComercial3 = dto.CodComercial3;
            entity.Descripcion = dto.Descripcion;
            entity.NomCorto = dto.NomCorto;
            entity.IdClase = dto.IdClase;
            entity.IdSClase = dto.IdSClase;
            entity.IdSSClase = dto.IdSSClase;
            entity.IdCc = dto.IdCc;
            entity.IdScc = dto.IdScc;
            entity.IdSscc = dto.IdSscc;
            entity.Estado = dto.Estado;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Productos.FindAsync(id);
            if (entity == null) return false;
            _context.Productos.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
} 