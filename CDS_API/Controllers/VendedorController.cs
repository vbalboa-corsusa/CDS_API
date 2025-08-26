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
    [SwaggerTag("Gestión de vendedores")]
    public class VendedorController : ControllerBase
    {
        private readonly IVendedorService _service;
        private readonly IMapper _mapper;
        public VendedorController(IVendedorService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtiene todos los vendedores")]
        [SwaggerResponse(200, "Lista de vendedores obtenida exitosamente", typeof(IEnumerable<VendedorDTO>))]
        public async Task<ActionResult<IEnumerable<VendedorDTO>>> GetAll()
        {
            System.Console.WriteLine("[LOG] GET /Vendedor llamado");
            try
            {
                var vendedores = await _service.GetAllAsync();
                System.Console.WriteLine($"[LOG] Vendedores encontrados: {vendedores.Count()}");
                return Ok(vendedores);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"[ERROR] GET /Vendedor: {ex.Message}\n{ex.StackTrace}");
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtiene un vendedor por ID")]
        [SwaggerResponse(200, "Vendedor encontrado", typeof(VendedorDTO))]
        [SwaggerResponse(404, "Vendedor no encontrado")]
        public async Task<ActionResult<VendedorDTO>> GetById(string id)
        {
            var vendedor = await _service.GetByIdAsync(id);
            if (vendedor == null) return NotFound();
            return Ok(vendedor);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Crea un nuevo vendedor")]
        [SwaggerResponse(201, "Vendedor creado", typeof(VendedorDTO))]
        public async Task<ActionResult<VendedorDTO>> Create(VendedorDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.IdVdr }, created);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Actualiza un vendedor existente")]
        [SwaggerResponse(204, "Vendedor actualizado")]
        [SwaggerResponse(400, "ID no coincide")]
        [SwaggerResponse(404, "Vendedor no encontrado")]
        public async Task<IActionResult> Update(string id, VendedorDTO dto)
        {
            if (id == dto.IdVdr)
            {
                var ok = await _service.UpdateAsync(id, dto);
                if (!ok) return NotFound();
                return NoContent();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Elimina un vendedor")]
        [SwaggerResponse(204, "Vendedor eliminado")]
        [SwaggerResponse(404, "Vendedor no encontrado")]
        public async Task<IActionResult> Delete(string id)
        {
            var ok = await _service.DeleteAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
