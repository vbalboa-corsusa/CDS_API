using CDS_BLL.Interfaces;
using CDS_DAL;
using CDS_Models;
using CDS_Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using CDS_Models.Entities;
using AutoMapper;

namespace CDS_BLL.Services
{
    public class ClienteService : IClienteService
    {
        private readonly LogistContext _context;
        private readonly IMapper _mapper;

        public ClienteService(LogistContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClienteDTO>> GetAllAsync()
        {
            try
            {
                var count = await _context.Clientes.CountAsync();
                System.Console.WriteLine($"CLIENTE count: {count}"); // Log para Railway
                var clientes = await _context.Clientes.ToListAsync();
                return _mapper.Map<IEnumerable<ClienteDTO>>(clientes);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"ERROR en GetAllAsync CLIENTE: {ex.Message}\n{ex.StackTrace}");
                throw; // Para que el controlador devuelva 500 y se vea el error en Railway
            }
        }

        public async Task<ClienteDTO?> GetByIdAsync(string id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) return null;
            return _mapper.Map<ClienteDTO>(cliente);
        }

        public async Task<ClienteDTO> CreateAsync(ClienteDTO dto)
        {
            string? lastId = await _context.Clientes
                .OrderByDescending(c => c.IdClt)
                .Select(c => c.IdClt)
                .FirstOrDefaultAsync();

            int maxId = 0;
            if (!string.IsNullOrEmpty(lastId))
            {
                maxId = int.Parse(lastId.Substring(3));
            }
            var entity = _mapper.Map<Cliente>(dto);
            entity.IdClt = "CLT" + (maxId + 1).ToString("D7");
            _context.Clientes.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<ClienteDTO>(entity);
        }

        public async Task<bool> UpdateAsync(string id, ClienteDTO dto)
        {
            var entity = await _context.Clientes.FindAsync(id);
            if (entity == null) return false;
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Clientes.FindAsync(id);
            if (entity == null) return false;
            _context.Clientes.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
