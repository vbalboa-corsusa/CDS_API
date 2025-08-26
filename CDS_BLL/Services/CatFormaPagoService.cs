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
    public class CatFormaPagoService : ICatFormaPagoService
    {
        private readonly LogistContext _context;
        private readonly IMapper _mapper;
        public CatFormaPagoService(LogistContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CatFormaPagoDTO>> GetAllAsync()
        {
            var catFormasPago = await _context.CatFormaPago.ToListAsync();
            return _mapper.Map<IEnumerable<CatFormaPagoDTO>>(catFormasPago);
        }

        public async Task<CatFormaPagoDTO?> GetByIdAsync(int id)
        {
            var catFormaPago = await _context.CatFormaPago.FindAsync(id);
            if (catFormaPago == null) return null;
            return _mapper.Map<CatFormaPagoDTO>(catFormaPago);
        }

        public async Task<CatFormaPagoDTO> CreateAsync(CatFormaPagoDTO dto)
        {
            var entity = _mapper.Map<CatFormaPago>(dto);
            _context.CatFormaPago.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CatFormaPagoDTO>(entity);
        }

        public async Task<bool> UpdateAsync(int id, CatFormaPagoDTO dto)
        {
            var entity = await _context.CatFormaPago.FindAsync(id);
            if (entity == null) return false;
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.CatFormaPago.FindAsync(id);
            if (entity == null) return false;
            _context.CatFormaPago.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
