using Microsoft.AspNetCore.Mvc;
using CDS_DAL;
using System.Collections.Generic;
using CDS_Models;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace CDS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [SwaggerTag("Gestión de monedas")]
    public class MonedaController : ControllerBase
    {
        private readonly LogistContext _context;

        public MonedaController(LogistContext context)
        {
            _context = context;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtiene todas las monedas")]
        [SwaggerResponse(200, "Lista de monedas obtenida exitosamente", typeof(IEnumerable<Moneda>))]
        [SwaggerResponse(404, "No se encontraron monedas")]
        public async Task<ActionResult<IEnumerable<Moneda>>> GetMonedas()
        {
            System.Console.WriteLine("[LOG] GET /Moneda llamado");
            try
            {
                if (_context.Monedas == null)
                {
                    System.Console.WriteLine("[LOG] _context.Monedas es null");
                    return NotFound();
                }
                var monedas = await _context.Monedas.ToListAsync();
                System.Console.WriteLine($"[LOG] Monedas encontradas: {monedas.Count}");
                return monedas;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"[ERROR] GET /Moneda: {ex.Message}\n{ex.StackTrace}");
                throw;
            }
        }
    }
}
