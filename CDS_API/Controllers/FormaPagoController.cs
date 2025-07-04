using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CDS_DAL;
using CDS_Models.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace CDS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormaPagoController : ControllerBase
    {
        private readonly LogistContext _context;
        public FormaPagoController(LogistContext context)
        {
            _context = context;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtiene todas las formas de pago")]
        [SwaggerResponse(200, "Lista de formas de pago obtenida exitosamente", typeof(IEnumerable<FormaPago>))]
        [SwaggerResponse(404, "No se encontraron formas de pago")]
        public async Task<ActionResult<IEnumerable<FormaPago>>> GetFormaPago()
        {
            System.Console.WriteLine("[LOG] GET /FormaPago llamado");
            try
            {
                if (_context.FormaPago == null)
                {
                    System.Console.WriteLine("[LOG] _context.FormaPago es null");
                    return NotFound();
                }
                var formas = await _context.FormaPago.ToListAsync();
                System.Console.WriteLine($"[LOG] Formas de pago encontradas: {formas.Count}");
                return formas;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"[ERROR] GET /FormaPago: {ex.Message}\n{ex.StackTrace}");
                throw;
            }
        }
    }
} 