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
    [SwaggerTag("Gestión de productos")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _service;

        private readonly IMapper _mapper;
        public ProductoController(IProductoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper; // Inyecta el mapeador de AutoMapper
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtiene todos los productos")]
        [SwaggerResponse(200, "Lista de productos obtenida exitosamente", typeof(IEnumerable<ProductoDTO>))]
        [SwaggerResponse(404, "No se encontraron productos")]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> GetAll()
        {
            System.Console.WriteLine("[LOG] GET /Producto llamado");
            try
            {   
                var productos = await _service.GetAllAsync();
                System.Console.WriteLine($"[LOG] Productos encontrados: {productos.Count()}");
                return Ok(productos);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"[ERROR] GET /Producto: {ex.Message}\n{ex.StackTrace}");
                throw;
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtiene un producto por ID")]
        [SwaggerResponse(200, "Producto encontrado", typeof(ProductoDTO))]
        [SwaggerResponse(404, "Producto no encontrado")]
        public async Task<ActionResult<ProductoDTO>> GetById(string id)
        {
            var producto = await _service.GetByIdAsync(id);
            if (producto == null) return NotFound();
            return Ok(producto);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Crea un nuevo producto")]
        [SwaggerResponse(201, "Producto creado", typeof(ProductoDTO))]
        [SwaggerResponse(400, "Datos inv�lidos")]
        public async Task<ActionResult<ProductoDTO>> Create(ProductoDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.IdPrd }, created);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Actualiza un producto existente")]
        [SwaggerResponse(204, "Producto actualizado")]
        [SwaggerResponse(400, "ID no coincide")]
        public async Task<IActionResult> Update(string id, ProductoDTO dto)
        {
            if (dto.IdPrd == null || id.ToString() != dto.IdPrd) return BadRequest();
            var ok = await _service.UpdateAsync(id, dto);
            if (!ok) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Elimina un producto por ID")]
        [SwaggerResponse(204, "Producto eliminado")]
        [SwaggerResponse(404, "Producto no encontrado")]
        public async Task<IActionResult> Delete(string id)
        {
            var ok = await _service.DeleteAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
