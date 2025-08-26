using Microsoft.AspNetCore.Mvc;
using CDS_BLL.Interfaces;
using CDS_Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;

namespace CDS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [SwaggerTag("Gestión de monedas")]
    public class MonedaController : ControllerBase
    {
        private readonly IMonedaService _monedaService;

        public MonedaController(IMonedaService monedaService)
        {
            _monedaService = monedaService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtiene todas las monedas")]
        [SwaggerResponse(200, "Lista de monedas obtenida exitosamente", typeof(IEnumerable<MonedaDTO>))]
        [SwaggerResponse(404, "No se encontraron monedas")]
        public async Task<ActionResult<IEnumerable<MonedaDTO>>> GetMonedas()
        {
            System.Console.WriteLine("[LOG] GET /Moneda llamado");
            try
            {
                var monedas = await _monedaService.GetAllAsync();
                System.Console.WriteLine($"[LOG] Monedas encontradas: {monedas.Count()}");
                return Ok(monedas);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"[ERROR] GET /Moneda: {ex.Message}\n{ex.StackTrace}");
                throw;
            }
        }
    }
}
