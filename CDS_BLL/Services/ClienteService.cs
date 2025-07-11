﻿using CDS_BLL.Interfaces;
using CDS_DAL;
using CDS_Models;
using CDS_Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace CDS_BLL.Services
{
    public class ClienteService : IClienteService
    {
        private readonly LogistContext _context;

        public ClienteService(LogistContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClienteDTO>> GetAllAsync()
        {
            try
            {
                var count = await _context.Clientes.CountAsync();
                System.Console.WriteLine($"CLIENTE count: {count}"); // Log para Railway
                return await _context.Clientes
                    .Select(c => new ClienteDTO
                    {
                        IdCliente = c.IdCliente,
                        IdTdi = c.IdTdi,
                        RazonSocial = c.RazonSocial,
                        CorreoCliente = c.CorreoCliente,
                        NumDocumento = c.NumDocumento,
                        TelefonoCliente = c.TelefonoCliente,
                        DireccionCliente = c.DireccionCliente,
                        IbCltPrv = c.IbCltPrv,
                        IbCltFinal = c.IbCltFinal
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"ERROR en GetAllAsync CLIENTE: {ex.Message}\n{ex.StackTrace}");
                throw; // Para que el controlador devuelva 500 y se vea el error en Railway
            }
        }

        public async Task<ClienteDTO?> GetByIdAsync(int id)
        {
            var c = await _context.Clientes.FindAsync(id);
            if (c == null) return null;
            return new ClienteDTO
            {
                IdCliente = c.IdCliente,
                IdTdi = c.IdTdi,
                RazonSocial = c.RazonSocial,
                CorreoCliente = c.CorreoCliente,
                NumDocumento = c.NumDocumento,
                TelefonoCliente = c.TelefonoCliente,
                DireccionCliente = c.DireccionCliente,
                IbCltPrv = c.IbCltPrv,
                IbCltFinal = c.IbCltFinal
            };
        }

        public async Task<ClienteDTO> CreateAsync(ClienteDTO dto)
        {
            var entity = new Cliente
            {
                IdTdi = dto.IdTdi,
                RazonSocial = dto.RazonSocial,
                CorreoCliente = dto.CorreoCliente,
                NumDocumento = dto.NumDocumento,
                TelefonoCliente = dto.TelefonoCliente,
                DireccionCliente = dto.DireccionCliente,
                IbCltPrv = dto.IbCltPrv,
                IbCltFinal = dto.IbCltFinal
            };
            _context.Clientes.Add(entity);
            await _context.SaveChangesAsync();
            dto.IdCliente = entity.IdCliente;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, ClienteDTO dto)
        {
            var entity = await _context.Clientes.FindAsync(id);
            if (entity == null) return false;
            entity.IdTdi = dto.IdTdi;
            entity.RazonSocial = dto.RazonSocial;
            entity.CorreoCliente = dto.CorreoCliente;
            entity.NumDocumento = dto.NumDocumento;
            entity.TelefonoCliente = dto.TelefonoCliente;
            entity.DireccionCliente = dto.DireccionCliente;
            entity.IbCltPrv = dto.IbCltPrv;
            entity.IbCltFinal = dto.IbCltFinal;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Clientes.FindAsync(id);
            if (entity == null) return false;
            _context.Clientes.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}