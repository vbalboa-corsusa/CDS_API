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
                    IdPrd = p.IdPrd,
                    IdMca = p.IdMca,
                    CodCom1 = p.CodCom1,
                    CodCom2 = p.CodCom2,
                    CodCom3 = p.CodCom3,
                    Descrip = p.Descrip,
                    NCorto = p.NCorto,
                    IdCls = p.IdCls,
                    IdSCls = p.IdSCls,
                    IdSsCls = p.IdSsCls,
                    IdCc = p.IdCc,
                    IdScC = p.IdScC,
                    IdSscC = p.IdSscC,
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
                IdPrd = p.IdPrd,
                IdMca = p.IdMca,
                CodCom1 = p.CodCom1,
                CodCom2 = p.CodCom2,
                CodCom3 = p.CodCom3,
                Descrip = p.Descrip,
                NCorto = p.NCorto,
                IdCls = p.IdCls,
                IdSCls = p.IdSCls,
                IdSsCls = p.IdSsCls,
                IdCc = p.IdCc,
                IdScC = p.IdScC,
                IdSscC = p.IdSscC,
                Estado = p.Estado
            };
        }

        public async Task<ProductoDTO> CreateAsync(ProductoDTO dto)
        {
            var entity = new Producto
            {
                IdPrd = dto.IdPrd,
                IdMca = dto.IdMca,
                CodCom1 = dto.CodCom1,
                CodCom2 = dto.CodCom2,
                CodCom3 = dto.CodCom3,
                Descrip = dto.Descrip,
                NCorto = dto.NCorto,
                IdCls = dto.IdCls,
                IdSCls = dto.IdSCls,
                IdSsCls = dto.IdSsCls,
                IdCc = dto.IdCc,
                IdScC = dto.IdScC,
                IdSscC = dto.IdSscC,
                Estado = dto.Estado
            };
            _context.Productos.Add(entity);
            await _context.SaveChangesAsync();
            dto.IdPrd = entity.IdPrd;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, ProductoDTO dto)
        {
            var entity = await _context.Productos.FindAsync(id);
            if (entity == null) return false;
            entity.IdMca = dto.IdMca;
            entity.CodCom1 = dto.CodCom1;
            entity.CodCom2 = dto.CodCom2;
            entity.CodCom3 = dto.CodCom3;
            entity.Descrip = dto.Descrip;
            entity.NCorto = dto.NCorto;
            entity.IdCls = dto.IdCls;
            entity.IdSCls = dto.IdSCls;
            entity.IdSsCls = dto.IdSsCls;
            entity.IdCc = dto.IdCc;
            entity.IdScC = dto.IdScC;
            entity.IdSscC = dto.IdSscC;
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
