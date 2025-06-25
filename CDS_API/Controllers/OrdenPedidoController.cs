using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CDS_DAL;
using CDS_Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CDS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdenPedidoController : ControllerBase
    {
        private readonly LogistContext _context;

        public OrdenPedidoController(LogistContext context)
        {
            _context = context;
        }

        // GET: /OrdenPedido
        [HttpGet]
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
        public async Task<ActionResult<OrdenPedido>> Create(OrdenPedido pedido)
        {
            _context.OrdenPedido.Add(pedido);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = pedido.IdOpci }, pedido);
        }

        // PUT: /OrdenPedido/{id}
        [HttpPut("{id}")]
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