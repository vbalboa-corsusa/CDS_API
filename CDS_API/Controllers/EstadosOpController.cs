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
    [SwaggerTag("Gestión de estados de operación")]
    public class EstadosOpController : ControllerBase
    {
        private readonly IStatusOpService _service;
        private readonly IMapper _mapper;
        public EstadosOpController(IStatusOpService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtiene todos los estados de operación")]
        [SwaggerResponse(200, "Lista de estados de operación obtenida exitosamente", typeof(IEnumerable<EstadosOpDTO>))]
        public async Task<ActionResult<IEnumerable<EstadosOpDTO>>> GetAll()
        {
            System.Console.WriteLine("[LOG] GET /StatusOp llamado");
            try
            {
                var statusOps = await _service.GetAllAsync();
                System.Console.WriteLine($"[LOG] Estados de operación encontrados: {statusOps.Count()}");
                return Ok(statusOps);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"[ERROR] GET /StatusOp: {ex.Message}\n{ex.StackTrace}");
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtiene un estado de operación por ID")]
        [SwaggerResponse(200, "Estado de operación encontrado", typeof(EstadosOpDTO))]
        [SwaggerResponse(404, "Estado de operación no encontrado")]
        public async Task<ActionResult<EstadosOpDTO>> GetById(int id)
        {
            var statusOp = await _service.GetByIdAsync(id);
            if (statusOp == null) return NotFound();
            return Ok(statusOp);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Crea un nuevo estado de operación")]
        [SwaggerResponse(201, "Estado de operación creado", typeof(EstadosOpDTO))]
        public async Task<ActionResult<EstadosOpDTO>> Create(EstadosOpDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.IdEstOp }, created);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Actualiza un estado de operación existente")]
        [SwaggerResponse(204, "Estado de operación actualizado")]
        [SwaggerResponse(400, "ID no coincide")]
        [SwaggerResponse(404, "Estado de operación no encontrado")]
        public async Task<IActionResult> Update(int id, EstadosOpDTO dto)
        {
            if (id == dto.IdEstOp)
            {
                var ok = await _service.UpdateAsync(id, dto);
                if (!ok) return NotFound();
                return NoContent();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Elimina un estado de operación")]
        [SwaggerResponse(204, "Estado de operación eliminado")]
        [SwaggerResponse(404, "Estado de operación no encontrado")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
