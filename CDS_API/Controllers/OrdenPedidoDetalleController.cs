using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CDS_DAL;
using CDS_Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;

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
            var detalles = await _context.OrdenesPedidoDetalle
                .Include(x => x.Producto)
                .Include(x => x.Servicio)
                .Include(x => x.Proyecto)
                .Include(x => x.UnidadMedida)
                .Include(x => x.Moneda)
                .Include(x => x.TcUsd)
                .Include(x => x.CCosto)
                .Include(x => x.ScCosto)
                .Include(x => x.SscCosto)
                .Include(x => x.OrdenPedido)
                .Include(x => x.SubSubTiposNegocio)
                .Include(x => x.SubTiposNegocio)
                .Include(x => x.TiposNegocio)
                .Include(x => x.StatusOp)
                .ToListAsync();
            return Ok(detalles);
        }

        // GET: /OrdenPedidoDetalle/{id}
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtiene un detalle de orden de pedido por ID")]
        [SwaggerResponse(200, "Detalle encontrado", typeof(OrdenPedidoDetalle))]
        [SwaggerResponse(404, "Detalle no encontrado")]
        public async Task<ActionResult<OrdenPedidoDetalle>> GetById(int id)
        {
            var detalle = await _context.OrdenesPedidoDetalle
                .Include(x => x.Producto)
                .Include(x => x.Servicio)
                .Include(x => x.Proyecto)
                .Include(x => x.UnidadMedida)
                .Include(x => x.Moneda)
                .Include(x => x.TcUsd)
                .Include(x => x.CCosto)
                .Include(x => x.ScCosto)
                .Include(x => x.SscCosto)
                .Include(x => x.OrdenPedido)
                .Include(x => x.SubSubTiposNegocio)
                .Include(x => x.SubTiposNegocio)
                .Include(x => x.TiposNegocio)
                .Include(x => x.StatusOp)
                .FirstOrDefaultAsync(x => x.IdOpd == id);
            if (detalle == null)
                return NotFound();
            return Ok(detalle);
        }

        // POST: /OrdenPedidoDetalle
        [HttpPost]
        [SwaggerOperation(Summary = "Crea un nuevo detalle de orden de pedido")]
        [SwaggerResponse(201, "Detalle creado exitosamente", typeof(OrdenPedidoDetalle))]
        [SwaggerResponse(400, "Solicitud inválida")]
        public async Task<ActionResult<OrdenPedidoDetalle>> Create(OrdenPedidoDetalle detalle)
        {
            _context.OrdenesPedidoDetalle.Add(detalle);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = detalle.IdOpd }, detalle);
        }

        // PUT: /OrdenPedidoDetalle/{id}
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Actualiza un detalle de orden de pedido existente")]
        [SwaggerResponse(204, "Detalle actualizado exitosamente")]
        [SwaggerResponse(400, "ID no coincide con el detalle proporcionado")]
        [SwaggerResponse(404, "Detalle no encontrado")]
        public async Task<IActionResult> Update(int id, OrdenPedidoDetalle detalle)
        {
            if (id != detalle.IdOpd)
                return BadRequest();
            _context.Entry(detalle).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.OrdenesPedidoDetalle.AnyAsync(x => x.IdOpd == id))
                    return NotFound();
                else
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
            var detalle = await _context.OrdenesPedidoDetalle.FindAsync(id);
            if (detalle == null)
                return NotFound();
            _context.OrdenesPedidoDetalle.Remove(detalle);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
} 