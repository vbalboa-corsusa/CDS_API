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
    [SwaggerTag("Gestión de productos")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _service;
        public ProductoController(IProductoService service)
        {
            _service = service;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtiene todos los productos")]
        [SwaggerResponse(200, "Lista de productos obtenida exitosamente", typeof(IEnumerable<ProductoDTO>))]
        [SwaggerResponse(404, "No se encontraron productos")]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> GetAll()
        {
            var productos = await _service.GetAllAsync();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtiene un producto por ID")]
        [SwaggerResponse(200, "Producto encontrado", typeof(ProductoDTO))]
        [SwaggerResponse(404, "Producto no encontrado")]
        public async Task<ActionResult<ProductoDTO>> GetById(int id)
        {
            var producto = await _service.GetByIdAsync(id);
            if (producto == null) return NotFound();
            return Ok(producto);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Crea un nuevo producto")]
        [SwaggerResponse(201, "Producto creado", typeof(ProductoDTO))]
        [SwaggerResponse(400, "Datos inválidos")]
        public async Task<ActionResult<ProductoDTO>> Create(ProductoDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.IdProd }, created);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Actualiza un producto existente")]
        [SwaggerResponse(204, "Producto actualizado")]
        [SwaggerResponse(400, "ID no coincide")]
        public async Task<IActionResult> Update(int id, ProductoDTO dto)
        {
            if (id != dto.IdProd) return BadRequest();
            var ok = await _service.UpdateAsync(id, dto);
            if (!ok) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Elimina un producto por ID")]
        [SwaggerResponse(204, "Producto eliminado")]
        [SwaggerResponse(404, "Producto no encontrado")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
} 