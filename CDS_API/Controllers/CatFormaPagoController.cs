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
    public class CatFormaPagoController : ControllerBase
    {
        private readonly ICatFormaPagoService _service;
        private readonly IMapper _mapper;
        public CatFormaPagoController(ICatFormaPagoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtiene todas las categorías de formas de pago")]
        [SwaggerResponse(200, "Lista de categorías de formas de pago obtenida exitosamente", typeof(IEnumerable<CatFormaPagoDTO>))]
        public async Task<ActionResult<IEnumerable<CatFormaPagoDTO>>> GetAll()
        {
            System.Console.WriteLine("[LOG] GET /CatFormaPago llamado");
            try
            {
                var catFormasPago = await _service.GetAllAsync();
                System.Console.WriteLine($"[LOG] Categorías de formas de pago encontradas: {catFormasPago.Count()}");
                return Ok(catFormasPago);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"[ERROR] GET /CatFormaPago: {ex.Message}\n{ex.StackTrace}");
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtiene una categoría de forma de pago por ID")]
        [SwaggerResponse(200, "Categoría de forma de pago encontrada", typeof(CatFormaPagoDTO))]
        [SwaggerResponse(404, "Categoría de forma de pago no encontrada")]
        public async Task<ActionResult<CatFormaPagoDTO>> GetById(int id)
        {
            var catFormaPago = await _service.GetByIdAsync(id);
            if (catFormaPago == null) return NotFound();
            return Ok(catFormaPago);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Crea una nueva categoría de forma de pago")]
        [SwaggerResponse(201, "Categoría de forma de pago creada", typeof(CatFormaPagoDTO))]
        public async Task<ActionResult<CatFormaPagoDTO>> Create(CatFormaPagoDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.IdCfp }, created);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Actualiza una categoría de forma de pago existente")]
        [SwaggerResponse(204, "Categoría de forma de pago actualizada")]
        [SwaggerResponse(400, "ID no coincide")]
        [SwaggerResponse(404, "Categoría de forma de pago no encontrada")]
        public async Task<IActionResult> Update(int id, CatFormaPagoDTO dto)
        {
            if (id == dto.IdCfp)
            {
                var ok = await _service.UpdateAsync(id, dto);
                if (!ok) return NotFound();
                return NoContent();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Elimina una categoría de forma de pago")]
        [SwaggerResponse(204, "Categoría de forma de pago eliminada")]
        [SwaggerResponse(404, "Categoría de forma de pago no encontrada")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
