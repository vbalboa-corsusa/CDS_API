using CDS_BLL.Interfaces;
using CDS_DAL;
using CDS_Models;
using CDS_Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDS_BLL.Services
{
    internal class ProyectoService : IProyectoService
    {
        private readonly LogistContext _context;

        public ProyectoService(LogistContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProyectoDTO>> GetAllAsync()
        {
            return await _context.Proyectos
                .Select(p => new ProyectoDTO
                {
                    IdPry = p.IdPry,
                    CodCom1 = p.CodCom1,
                    Descrip = p.Descrip,
                    NCorto = p.NCorto,
                    IdCc = p.IdCc,
                    IdScC = p.IdScC,
                    IdSscC = p.IdSscC,
                    Estado = p.Estado
                })
                .ToListAsync();
        }

        public async Task<ProyectoDTO?> GetByIdAsync(string id)
        {
            var p = await _context.Proyectos.FindAsync(id);
            if (p == null) return null;
            return new ProyectoDTO
            {
                IdPry = p.IdPry,
                CodCom1 = p.CodCom1,
                Descrip = p.Descrip,
                NCorto = p.NCorto,
                IdCc = p.IdCc,
                IdScC = p.IdScC,
                IdSscC = p.IdSscC,
                Estado = p.Estado
            };
        }

        public async Task<ProyectoDTO> CreateAsync(ProyectoDTO dto)
        {
            // Obtiene el último IdPry existente
            string? lastId = await _context.Proyectos
                .OrderByDescending(p => p.IdPry)
                .Select(p => p.IdPry)
                .FirstOrDefaultAsync();
            // Obtener el máximo IdProyecto existente (incluyendo eliminados lógicamente)
            int maxId = 0;
            if (!string.IsNullOrEmpty(lastId))
            {
                maxId = int.Parse(lastId.Substring(3)); // Asumiendo que el formato es "PRY001", "PRY002", etc.
            }
            // Crear nuevo proyecto
            var entity = new Proyecto
            {
                IdPry = "PRY" + (maxId +1).ToString("D7"),
                CodCom1 = dto.CodCom1,
                Descrip = dto.Descrip,
                NCorto = dto.NCorto,
                IdCc = dto.IdCc,
                IdScC = dto.IdScC,
                IdSscC = dto.IdSscC,
                Estado = dto.Estado
            };
            _context.Proyectos.Add(entity);
            await _context.SaveChangesAsync();
            dto.IdPry = entity.IdPry; // Asignar el Id generado al DTO
            return dto;

        }

        public async Task<ProyectoDTO> UpdateAsync(string id, ProyectoDTO dto)
        {
            var entity = await _context.Proyectos.FindAsync(id);
            if (entity == null) throw new KeyNotFoundException($"Proyecto con ID {id} no encontrado");// Manejo de error si no se encuentra el proyecto
            entity.CodCom1 = dto.CodCom1;
            entity.Descrip = dto.Descrip;
            entity.NCorto = dto.NCorto;
            entity.IdCc = dto.IdCc;
            entity.IdScC = dto.IdScC;
            entity.IdSscC = dto.IdSscC;
            entity.Estado = dto.Estado;
            _context.Proyectos.Update(entity);
            await _context.SaveChangesAsync();
            return dto;// Retornar el DTO actualizado
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Proyectos.FindAsync(id);
            if (entity == null) return false; // Si no encuentra el proyecto, retornar false
            _context.Proyectos.Remove(entity);
            await _context.SaveChangesAsync();
            return true; // Retornar true si eliminó exitosamente
        }

    }
}