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
    public class StatusOpService : IStatusOpService
    {
        private readonly LogistContext _context;
        private readonly IMapper _mapper;
        public StatusOpService(LogistContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EstadosOpDTO>> GetAllAsync()
        {
            var statusOps = await _context.StatusOp.ToListAsync();
            return _mapper.Map<IEnumerable<EstadosOpDTO>>(statusOps);
        }

        public async Task<EstadosOpDTO?> GetByIdAsync(int id)
        {
            var statusOp = await _context.StatusOp.FindAsync(id);
            if (statusOp == null) return null;
            return _mapper.Map<EstadosOpDTO>(statusOp);
        }

        public async Task<EstadosOpDTO> CreateAsync(EstadosOpDTO dto)
        {
            var entity = _mapper.Map<EstadosOp>(dto);
            _context.StatusOp.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<EstadosOpDTO>(entity);
        }

        public async Task<bool> UpdateAsync(int id, EstadosOpDTO dto)
        {
            var entity = await _context.StatusOp.FindAsync(id);
            if (entity == null) return false;
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.StatusOp.FindAsync(id);
            if (entity == null) return false;
            _context.StatusOp.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
