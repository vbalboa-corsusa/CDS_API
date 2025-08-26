using Microsoft.AspNetCore.Mvc;
using CDS_BLL.Interfaces;
using CDS_Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Linq;
using AutoMapper;

namespace CDS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormaPagoController : ControllerBase
    {
        private readonly IFormaPagoService _service;
        private readonly IMapper _mapper;
        public FormaPagoController(IFormaPagoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtiene todas las formas de pago")]
        [SwaggerResponse(200, "Lista de formas de pago obtenida exitosamente", typeof(IEnumerable<FormaPagoDTO>))]
        [SwaggerResponse(404, "No se encontraron formas de pago")]
        public async Task<ActionResult<IEnumerable<FormaPagoDTO>>> GetFormaPago()
        {
            System.Console.WriteLine("[LOG] GET /FormaPago llamado");
            try
            {
                var formas = await _service.GetAllAsync();
                System.Console.WriteLine($"[LOG] Formas de pago encontradas: {formas.Count()}");
                return Ok(formas);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"[ERROR] GET /FormaPago: {ex.Message}\n{ex.StackTrace}");
                throw;
            }
        }
    }
}
