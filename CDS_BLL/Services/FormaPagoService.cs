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
    public class FormaPagoService : IFormaPagoService
    {
        private readonly LogistContext _context;
        private readonly IMapper _mapper;
        public FormaPagoService(LogistContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FormaPagoDTO>> GetAllAsync()
        {
            var formasPago = await _context.FormaPago.ToListAsync();
            return _mapper.Map<IEnumerable<FormaPagoDTO>>(formasPago);
        }

        public async Task<FormaPagoDTO?> GetByIdAsync(int id)
        {
            var formaPago = await _context.FormaPago.FindAsync(id);
            if (formaPago == null) return null;
            return _mapper.Map<FormaPagoDTO>(formaPago);
        }

        public async Task<FormaPagoDTO> CreateAsync(FormaPagoDTO dto)
        {
            var entity = _mapper.Map<FormaPago>(dto);
            _context.FormaPago.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<FormaPagoDTO>(entity);
        }

        public async Task<bool> UpdateAsync(int id, FormaPagoDTO dto)
        {
            var entity = await _context.FormaPago.FindAsync(id);
            if (entity == null) return false;
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.FormaPago.FindAsync(id);
            if (entity == null) return false;
            _context.FormaPago.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
