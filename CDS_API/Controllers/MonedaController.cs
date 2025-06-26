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
            if (_context.Monedas == null)
            {
                return NotFound();
            }
            return await _context.Monedas.ToListAsync();
        }
    }
}
