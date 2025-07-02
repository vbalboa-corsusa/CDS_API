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
    [SwaggerTag("Gestión de clientes")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _service;
        public ClienteController(IClienteService service)
        {
            _service = service;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtiene todos los clientes")]
        [SwaggerResponse(200, "Lista de clientes obtenida exitosamente", typeof(IEnumerable<ClienteDTO>))]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetAll()
        {
            var clientes = await _service.GetAllAsync();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtiene un cliente por ID")]
        [SwaggerResponse(200, "Cliente encontrado", typeof(ClienteDTO))]
        [SwaggerResponse(404, "Cliente no encontrado")]
        public async Task<ActionResult<ClienteDTO>> GetById(int id)
        {
            var cliente = await _service.GetByIdAsync(id);
            if (cliente == null) return NotFound();
            return Ok(cliente);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Crea un nuevo cliente")]
        [SwaggerResponse(201, "Cliente creado", typeof(ClienteDTO))]
        public async Task<ActionResult<ClienteDTO>> Create(ClienteDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.IdCliente }, created);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Actualiza un cliente existente")]
        [SwaggerResponse(204, "Cliente actualizado")]
        [SwaggerResponse(400, "ID no coincide")]
        [SwaggerResponse(404, "Cliente no encontrado")]
        public async Task<IActionResult> Update(int id, ClienteDTO dto)
        {
            if (id != dto.IdCliente) return BadRequest();
            var ok = await _service.UpdateAsync(id, dto);
            if (!ok) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Elimina un cliente")]
        [SwaggerResponse(204, "Cliente eliminado")]
        [SwaggerResponse(404, "Cliente no encontrado")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
} 