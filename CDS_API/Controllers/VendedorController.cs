using Microsoft.AspNetCore.Mvc;
using CDS_BLL.Interfaces;
using CDS_Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CDS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendedorController : ControllerBase
    {
        private readonly IVendedorService _service;
        public VendedorController(IVendedorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VendedorDTO>>> GetAll()
        {
            var vendedores = await _service.GetAllAsync();
            return Ok(vendedores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VendedorDTO>> GetById(int id)
        {
            var vendedor = await _service.GetByIdAsync(id);
            if (vendedor == null) return NotFound();
            return Ok(vendedor);
        }

        [HttpPost]
        public async Task<ActionResult<VendedorDTO>> Create(VendedorDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.IdVendedor }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, VendedorDTO dto)
        {
            if (id != dto.IdVendedor) return BadRequest();
            var ok = await _service.UpdateAsync(id, dto);
            if (!ok) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
} 