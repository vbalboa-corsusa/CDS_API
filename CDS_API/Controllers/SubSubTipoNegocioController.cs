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
    [SwaggerTag("Gestión de subsubtipos de negocio")]
    public class SubSubTipoNegocioController : ControllerBase
    {
        private readonly ISubSubTipoNegocioService _service;
        private readonly IMapper _mapper;
        public SubSubTipoNegocioController(ISubSubTipoNegocioService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtiene todos los subsubtipos de negocio")]
        [SwaggerResponse(200, "Lista de subsubtipos de negocio obtenida exitosamente", typeof(IEnumerable<SubSubTipoNegocioDTO>))]
        public async Task<ActionResult<IEnumerable<SubSubTipoNegocioDTO>>> GetAll()
        {
            System.Console.WriteLine("[LOG] GET /SubSubTipoNegocio llamado");
            try
            {
                var subSubTiposNegocio = await _service.GetAllAsync();
                System.Console.WriteLine($"[LOG] Subsubtipos de negocio encontrados: {subSubTiposNegocio.Count()}");
                return Ok(subSubTiposNegocio);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"[ERROR] GET /SubSubTipoNegocio: {ex.Message}\n{ex.StackTrace}");
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtiene un subsubtipo de negocio por ID")]
        [SwaggerResponse(200, "Subsubtipo de negocio encontrado", typeof(SubSubTipoNegocioDTO))]
        [SwaggerResponse(404, "Subsubtipo de negocio no encontrado")]
        public async Task<ActionResult<SubSubTipoNegocioDTO>> GetById(int id)
        {
            var subSubTipoNegocio = await _service.GetByIdAsync(id);
            if (subSubTipoNegocio == null) return NotFound();
            return Ok(subSubTipoNegocio);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Crea un nuevo subsubtipo de negocio")]
        [SwaggerResponse(201, "Subsubtipo de negocio creado", typeof(SubSubTipoNegocioDTO))]
        public async Task<ActionResult<SubSubTipoNegocioDTO>> Create(SubSubTipoNegocioDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.IdSstn }, created);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Actualiza un subsubtipo de negocio existente")]
        [SwaggerResponse(204, "Subsubtipo de negocio actualizado")]
        [SwaggerResponse(400, "ID no coincide")]
        [SwaggerResponse(404, "Subsubtipo de negocio no encontrado")]
        public async Task<IActionResult> Update(int id, SubSubTipoNegocioDTO dto)
        {
            if (id == dto.IdSstn)
            {
                var ok = await _service.UpdateAsync(id, dto);
                if (!ok) return NotFound();
                return NoContent();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Elimina un subsubtipo de negocio")]
        [SwaggerResponse(204, "Subsubtipo de negocio eliminado")]
        [SwaggerResponse(404, "Subsubtipo de negocio no encontrado")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
