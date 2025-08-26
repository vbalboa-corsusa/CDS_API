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
    [SwaggerTag("Gestión de tipos de negocio")]
    public class TipoNegocioController : ControllerBase
    {
        private readonly ITipoNegocioService _service;
        private readonly IMapper _mapper;
        public TipoNegocioController(ITipoNegocioService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtiene todos los tipos de negocio")]
        [SwaggerResponse(200, "Lista de tipos de negocio obtenida exitosamente", typeof(IEnumerable<TipoNegocioDTO>))]
        public async Task<ActionResult<IEnumerable<TipoNegocioDTO>>> GetAll()
        {
            System.Console.WriteLine("[LOG] GET /TipoNegocio llamado");
            try
            {
                var tiposNegocio = await _service.GetAllAsync();
                System.Console.WriteLine($"[LOG] Tipos de negocio encontrados: {tiposNegocio.Count()}");
                return Ok(tiposNegocio);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"[ERROR] GET /TipoNegocio: {ex.Message}\n{ex.StackTrace}");
                return StatusCode(500, $"Error interno: {ex.Message}");
            }

        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtiene un tipo de negocio por ID")]
        [SwaggerResponse(200, "Tipo de negocio encontrado", typeof(TipoNegocioDTO))]
        [SwaggerResponse(404, "Tipo de negocio no encontrado")]
        public async Task<ActionResult<TipoNegocioDTO>> GetById(int id)
        {
            var tipoNegocio = await _service.GetByIdAsync(id);
            if (tipoNegocio == null) return NotFound();
            return Ok(tipoNegocio);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Crea un nuevo tipo de negocio")]
        [SwaggerResponse(201, "Tipo de negocio creado", typeof(TipoNegocioDTO))]
        public async Task<ActionResult<TipoNegocioDTO>> Create(TipoNegocioDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.IdTn }, created);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Actualiza un tipo de negocio existente")]
        [SwaggerResponse(204, "Tipo de negocio actualizado")]
        [SwaggerResponse(400, "ID no coincide")]
        [SwaggerResponse(404, "Tipo de negocio no encontrado")]
        public async Task<IActionResult> Update(int id, TipoNegocioDTO dto)
        {
            if (id == dto.IdTn)
            {
                var ok = await _service.UpdateAsync(id, dto);
                if (!ok) return NotFound();
                return NoContent();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Elimina un tipo de negocio")]
        [SwaggerResponse(204, "Tipo de negocio eliminado")]
        [SwaggerResponse(404, "Tipo de negocio no encontrado")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
