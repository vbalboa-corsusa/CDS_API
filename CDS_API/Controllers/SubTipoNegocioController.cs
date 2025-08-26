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
    [SwaggerTag("Gestión de subtipos de negocio")]
    public class SubTipoNegocioController : ControllerBase
    {
        private readonly ISubTipoNegocioService _service;
        private readonly IMapper _mapper;
        public SubTipoNegocioController(ISubTipoNegocioService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtiene todos los subtipos de negocio")]
        [SwaggerResponse(200, "Lista de subtipos de negocio obtenida exitosamente", typeof(IEnumerable<SubTipoNegocioDTO>))]
        public async Task<ActionResult<IEnumerable<SubTipoNegocioDTO>>> GetAll()
        {
            System.Console.WriteLine("[LOG] GET /SubTipoNegocio llamado");
            try
            {
                var subTiposNegocio = await _service.GetAllAsync();
                System.Console.WriteLine($"[LOG] Subtipos de negocio encontrados: {subTiposNegocio.Count()}");
                return Ok(subTiposNegocio);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"[ERROR] GET /SubTipoNegocio: {ex.Message}\n{ex.StackTrace}");
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtiene un subtipo de negocio por ID")]
        [SwaggerResponse(200, "Subtipo de negocio encontrado", typeof(SubTipoNegocioDTO))]
        [SwaggerResponse(404, "Subtipo de negocio no encontrado")]
        public async Task<ActionResult<SubTipoNegocioDTO>> GetById(int id)
        {
            var subTipoNegocio = await _service.GetByIdAsync(id);
            if (subTipoNegocio == null) return NotFound();
            return Ok(subTipoNegocio);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Crea un nuevo subtipo de negocio")]
        [SwaggerResponse(201, "Subtipo de negocio creado", typeof(SubTipoNegocioDTO))]
        public async Task<ActionResult<SubTipoNegocioDTO>> Create(SubTipoNegocioDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.IdStn }, created);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Actualiza un subtipo de negocio existente")]
        [SwaggerResponse(204, "Subtipo de negocio actualizado")]
        [SwaggerResponse(400, "ID no coincide")]
        [SwaggerResponse(404, "Subtipo de negocio no encontrado")]
        public async Task<IActionResult> Update(int id, SubTipoNegocioDTO dto)
        {
            if (id == dto.IdStn)
            {
                var ok = await _service.UpdateAsync(id, dto);
                if (!ok) return NotFound();
                return NoContent();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Elimina un subtipo de negocio")]
        [SwaggerResponse(204, "Subtipo de negocio eliminado")]
        [SwaggerResponse(404, "Subtipo de negocio no encontrado")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
