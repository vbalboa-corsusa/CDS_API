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
    [SwaggerTag("Gestión de tipos de documento")]
    public class TipoDocsIdentController : ControllerBase
    {
        private readonly ITipoDocumentoService _service;
        private readonly IMapper _mapper;
        public TipoDocsIdentController(ITipoDocumentoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtiene todos los tipos de documento")]
        [SwaggerResponse(200, "Lista de tipos de documento obtenida exitosamente", typeof(IEnumerable<TipoDocsIdentDTO>))]
        public async Task<ActionResult<IEnumerable<TipoDocsIdentDTO>>> GetAll()
        {
            System.Console.WriteLine("[LOG] GET /TipoDocumento llamado");
            try
            {
                var tiposDocumento = await _service.GetAllAsync();
                System.Console.WriteLine($"[LOG] Tipos de documento encontrados: {tiposDocumento.Count()}");
                return Ok(tiposDocumento);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"[ERROR] GET /TipoDocumento: {ex.Message}\n{ex.StackTrace}");
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtiene un tipo de documento por ID")]
        [SwaggerResponse(200, "Tipo de documento encontrado", typeof(TipoDocsIdentDTO))]
        [SwaggerResponse(404, "Tipo de documento no encontrado")]
        public async Task<ActionResult<TipoDocsIdentDTO>> GetById(int id)
        {
            var tipoDocumento = await _service.GetByIdAsync(id);
            if (tipoDocumento == null) return NotFound();
            return Ok(tipoDocumento);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Crea un nuevo tipo de documento")]
        [SwaggerResponse(201, "Tipo de documento creado", typeof(TipoDocsIdentDTO))]
        public async Task<ActionResult<TipoDocsIdentDTO>> Create(TipoDocsIdentDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.IdTdi }, created);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Actualiza un tipo de documento existente")]
        [SwaggerResponse(204, "Tipo de documento actualizado")]
        [SwaggerResponse(400, "ID no coincide")]
        [SwaggerResponse(404, "Tipo de documento no encontrado")]
        public async Task<IActionResult> Update(int id, TipoDocsIdentDTO dto)
        {
            if (id == dto.IdTdi)
            {
                var ok = await _service.UpdateAsync(id, dto);
                if (!ok) return NotFound();
                return NoContent();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Elimina un tipo de documento")]
        [SwaggerResponse(204, "Tipo de documento eliminado")]
        [SwaggerResponse(404, "Tipo de documento no encontrado")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
