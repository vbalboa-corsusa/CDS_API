using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDS_BLL.Interfaces;
using CDS_Models.DTOs;
using CDS_Models;
using CDS_DAL;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace CDS_BLL.Services
{
    public class ProductoService : IProductoService
    {
        private readonly LogistContext _context;
        private readonly IMapper _mapper;

        public ProductoService(LogistContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductoDTO>> GetAllAsync()
        {
            var productos = await _context.Productos.ToListAsync();
            return _mapper.Map<IEnumerable<ProductoDTO>>(productos);
        }

        public async Task<ProductoDTO?> GetByIdAsync(string id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return null;
            return _mapper.Map<ProductoDTO>(producto);
        }

        public async Task<ProductoDTO> CreateAsync(ProductoDTO dto)
        {
            string? lastId = await _context.Productos
                .OrderByDescending(p => p.IdPrd)
                .Select(p => p.IdPrd)
                .FirstOrDefaultAsync();

            int maxId = 0;
            if (!string.IsNullOrEmpty(lastId))
            {
                maxId = int.Parse(lastId.Substring(3));
            }

            var entity = _mapper.Map<Producto>(dto);
            entity.IdPrd = "PRD" + (maxId + 1).ToString("D7");
            _context.Productos.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductoDTO>(entity);
        }

        public async Task<bool> UpdateAsync(string id, ProductoDTO dto)
        {
            var entity = await _context.Productos.FindAsync(id);
            if (entity == null) return false;

            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Productos.FindAsync(id);
            if (entity == null) return false;
            _context.Productos.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
