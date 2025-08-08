using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CDS_DAL;
using CDS_Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Linq;
using CDS_Models.DTOs;

namespace CDS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [SwaggerTag("Gestión de detalles de pedidos")]
    public class OrdenPedidoDetalleController : ControllerBase
    {
        private readonly LogistContext _context;

        public OrdenPedidoDetalleController(LogistContext context)
        {
            _context = context;
        }

        // GET: /OrdenPedidoDetalle
        [HttpGet]
        [SwaggerOperation(Summary = "Obtiene todos los detalles de ordenes de pedido")]
        [SwaggerResponse(200, "Lista de detalles obtenida exitosamente", typeof(IEnumerable<OrdenPedidoDetalle>))]
        [SwaggerResponse(404, "No se encontraron detalles de ordenes de pedido")]
        public async Task<ActionResult<IEnumerable<OrdenPedidoDetalle>>> GetAll()
        {
            System.Console.WriteLine("[LOG] GET /OrdenPedidoDetalle llamado");
            try
            {
                var detalles = await _context.OrdenesPedidoDetalle
                    .Include(x => x.Moneda)
                    .Include(x => x.TcUsd)
                    .Include(x => x.CCosto)
                    .Include(x => x.ScCosto)
                    .Include(x => x.SscCosto)
                    .Include(x => x.OrdenPedido)
                    .Include(x => x.SubSubTiposNegocio)
                    .Include(x => x.SubTiposNegocio)
                    .Include(x => x.TipoNegocio)
                    .ToListAsync();
                System.Console.WriteLine($"[LOG] Ordenes de pedido detalle encontradas: {detalles.Count}");
                return Ok(detalles);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"[ERROR] GET /OrdenPedidoDetalle: {ex.Message}\n{ex.StackTrace}");
                throw;
            }
        }

        // GET: /OrdenPedidoDetalle/{id}
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtiene un detalle de orden de pedido por ID")]
        [SwaggerResponse(200, "Detalle encontrado", typeof(OrdenPedidoDetalle))]
        [SwaggerResponse(404, "Detalle no encontrado")]
        public async Task<ActionResult<OrdenPedidoDetalle>> GetById(string id)
        {
            System.Console.WriteLine($"[LOG] GET /OrdenPedidoDetalle/{id} llamado");
            try
            {
                var detalle = await _context.OrdenesPedidoDetalle
                    .Include(x => x.Moneda)
                    .Include(x => x.TcUsd)
                    .Include(x => x.CCosto)
                    .Include(x => x.ScCosto)
                    .Include(x => x.SscCosto)
                    .Include(x => x.OrdenPedido)
                    .Include(x => x.SubSubTiposNegocio)
                    .Include(x => x.SubTiposNegocio)
                    .Include(x => x.TipoNegocio)
                    //.Include(x => x.EstadosOp)
                    .FirstOrDefaultAsync(x => x.IdOpci == id);
                if (detalle == null)
                {
                    System.Console.WriteLine($"[LOG] Detalle con ID {id} no encontrado");
                    return NotFound();
                }
                System.Console.WriteLine($"[LOG] Detalle con ID {id} encontrado");
                return Ok(detalle);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"[ERROR] GET /OrdenPedidoDetalle/{id}: {ex.Message}\n{ex.StackTrace}");
                throw;
            }
        }

        // POST: /OrdenPedidoDetalle
        [HttpPost]
        [SwaggerOperation(Summary = "Crea un nuevo detalle de orden de pedido")]
        [SwaggerResponse(201, "Detalle creado exitosamente", typeof(OrdenPedidoDetalle))]
        [SwaggerResponse(400, "Solicitud inválida")]
        public async Task<ActionResult<OrdenPedidoDetalle>> Create(OrdenPedidoDetalle detalle)
        {
            System.Console.WriteLine("[LOG] POST /OrdenPedidoDetalle llamado");
            try
            {
                _context.OrdenesPedidoDetalle.Add(detalle);
                await _context.SaveChangesAsync();
                System.Console.WriteLine($"[LOG] Detalle con ID {detalle.IdOpci} creado");
                return CreatedAtAction(nameof(GetById), new { id = detalle.IdOpci }, detalle);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"[ERROR] POST /OrdenPedidoDetalle: {ex.Message}\n{ex.StackTrace}");
                throw;
            }
        }

        // PUT: /OrdenPedidoDetalle/{id}
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Actualiza un detalle de orden de pedido existente")]
        [SwaggerResponse(204, "Detalle actualizado exitosamente")]
        [SwaggerResponse(400, "ID no coincide con el detalle proporcionado")]
        [SwaggerResponse(404, "Detalle no encontrado")]
        public async Task<IActionResult> Update(string id, OrdenPedidoDetalle detalle)
        {
            System.Console.WriteLine($"[LOG] PUT /OrdenPedidoDetalle/{id} llamado");
            if (id != detalle.IdOpci)
            {
                System.Console.WriteLine($"[ERROR] PUT /OrdenPedidoDetalle/{id}: ID no coincide con el detalle proporcionado");
                return BadRequest();
            }
            _context.Entry(detalle).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                System.Console.WriteLine($"[LOG] Detalle con ID {id} actualizado");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.OrdenesPedidoDetalle.AnyAsync(x => x.IdOpci == id))
                {
                    System.Console.WriteLine($"[ERROR] PUT /OrdenPedidoDetalle/{id}: Detalle no encontrado");
                    return NotFound();
                }
                else
                {
                    System.Console.WriteLine($"[ERROR] PUT /OrdenPedidoDetalle/{id}: Error de concurrencia de datos");
                    throw;
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"[ERROR] PUT /OrdenPedidoDetalle/{id}: {ex.Message}\n{ex.StackTrace}");
                throw;
            }
            return NoContent();
        }

        // DELETE: /OrdenPedidoDetalle/{id}
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Elimina un detalle de orden de pedido por ID")]
        [SwaggerResponse(204, "Detalle eliminado exitosamente")]
        [SwaggerResponse(404, "Detalle no encontrado")]
        public async Task<IActionResult> Delete(int id)
        {
            System.Console.WriteLine($"[LOG] DELETE /OrdenPedidoDetalle/{id} llamado");
            try
            {
                var detalle = await _context.OrdenesPedidoDetalle.FindAsync(id);
                if (detalle == null)
                {
                    System.Console.WriteLine($"[LOG] Detalle con ID {id} no encontrado para eliminar");
                    return NotFound();
                }
                _context.OrdenesPedidoDetalle.Remove(detalle);
                await _context.SaveChangesAsync();
                System.Console.WriteLine($"[LOG] Detalle con ID {id} eliminado");
                return NoContent();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"[ERROR] DELETE /OrdenPedidoDetalle/{id}: {ex.Message}\n{ex.StackTrace}");
                throw;
            }
        }
    }
}
