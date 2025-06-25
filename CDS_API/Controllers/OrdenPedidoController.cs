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
    [SwaggerTag("Gestión de órdenes de pedido")]
    public class OrdenPedidoController : ControllerBase
    {
        private readonly LogistContext _context;

        public OrdenPedidoController(LogistContext context)
        {
            _context = context;
        }

        // GET: /OrdenPedido
        [HttpGet]
        [SwaggerOperation(Summary = "Obtiene todas las órdenes de pedido")]
        [SwaggerResponse(200, "Lista de órdenes de pedido obtenida exitosamente", typeof(IEnumerable<OrdenPedido>))]
        [SwaggerResponse(404, "No se encontraron órdenes de pedido")]
        public async Task<ActionResult<IEnumerable<OrdenPedido>>> GetAll()
        {
            var pedidos = await _context.OrdenPedido
                .Include(x => x.FormaPago)
                .Include(x => x.Cliente)
                .Include(x => x.Vendedor)
                .Include(x => x.Moneda)
                .Include(x => x.OrdenPedidoDetalles)
                .ToListAsync();
            return Ok(pedidos);
        }

        // GET: /OrdenPedido/{id}
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtiene una orden de pedido por ID")]
        [SwaggerResponse(200, "Orden de pedido encontrada", typeof(OrdenPedido))]
        [SwaggerResponse(404, "Orden de pedido no encontrada")]
        public async Task<ActionResult<OrdenPedido>> GetById(int id)
        {
            var pedido = await _context.OrdenPedido
                .Include(x => x.FormaPago)
                .Include(x => x.Cliente)
                .Include(x => x.Vendedor)
                .Include(x => x.Moneda)
                .Include(x => x.OrdenPedidoDetalles)
                .FirstOrDefaultAsync(x => x.IdOpci == id);
            if (pedido == null)
                return NotFound();
            return Ok(pedido);
        }

        // POST: /OrdenPedido
        [HttpPost]
        [SwaggerOperation(Summary = "Crea una nueva orden de pedido")]
        [SwaggerResponse(201, "Orden de pedido creada exitosamente", typeof(OrdenPedido))]
        [SwaggerResponse(400, "Datos inválidos")]
        public async Task<ActionResult<OrdenPedido>> Create(OrdenPedido pedido)
        {
            _context.OrdenPedido.Add(pedido);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = pedido.IdOpci }, pedido);
        }

        // PUT: /OrdenPedido/{id}
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Actualiza una orden de pedido existente")]
        [SwaggerResponse(204, "Orden de pedido actualizada exitosamente")]
        [SwaggerResponse(400, "ID no coincide")]
        [SwaggerResponse(404, "Orden de pedido no encontrada")]
        public async Task<IActionResult> Update(int id, OrdenPedido pedido)
        {
            if (id != pedido.IdOpci)
                return BadRequest();
            _context.Entry(pedido).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.OrdenPedido.AnyAsync(x => x.IdOpci == id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent();
        }

        // DELETE: /OrdenPedido/{id}
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Elimina una orden de pedido por ID")]
        [SwaggerResponse(204, "Orden de pedido eliminada exitosamente")]
        [SwaggerResponse(404, "Orden de pedido no encontrada")]
        public async Task<IActionResult> Delete(int id)
        {
            var pedido = await _context.OrdenPedido.FindAsync(id);
            if (pedido == null)
                return NotFound();
            _context.OrdenPedido.Remove(pedido);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
} 