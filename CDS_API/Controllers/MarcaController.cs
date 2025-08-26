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
    [SwaggerTag("Gestión de marcas")]
    public class MarcaController : ControllerBase
    {
        private readonly IMarcaService _service;
        private readonly IMapper _mapper;
        public MarcaController(IMarcaService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtiene todas las marcas")]
        [SwaggerResponse(200, "Lista de marcas obtenida exitosamente", typeof(IEnumerable<MarcaDTO>))]
        public async Task<ActionResult<IEnumerable<MarcaDTO>>> GetAll()
        {
            System.Console.WriteLine("[LOG] GET /Marca llamado");
            try
            {
                var marcas = await _service.GetAllAsync();
                System.Console.WriteLine($"[LOG] Marcas encontradas: {marcas.Count()}");
                return Ok(marcas);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"[ERROR] GET /Marca: {ex.Message}\n{ex.StackTrace}");
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtiene una marca por ID")]
        [SwaggerResponse(200, "Marca encontrada", typeof(MarcaDTO))]
        [SwaggerResponse(404, "Marca no encontrada")]
        public async Task<ActionResult<MarcaDTO>> GetById(int id)
        {
            var marca = await _service.GetByIdAsync(id);
            if (marca == null) return NotFound();
            return Ok(marca);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Crea una nueva marca")]
        [SwaggerResponse(201, "Marca creada", typeof(MarcaDTO))]
        public async Task<ActionResult<MarcaDTO>> Create(MarcaDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.IdMca }, created);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Actualiza una marca existente")]
        [SwaggerResponse(204, "Marca actualizada")]
        [SwaggerResponse(400, "ID no coincide")]
        [SwaggerResponse(404, "Marca no encontrada")]
        public async Task<IActionResult> Update(int id, MarcaDTO dto)
        {
            if (id == dto.IdMca)
            {
                var ok = await _service.UpdateAsync(id, dto);
                if (!ok) return NotFound();
                return NoContent();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Elimina una marca")]
        [SwaggerResponse(204, "Marca eliminada")]
        [SwaggerResponse(404, "Marca no encontrada")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
