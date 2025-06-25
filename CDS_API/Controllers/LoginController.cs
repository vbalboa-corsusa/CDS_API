using Microsoft.AspNetCore.Mvc;
using CDS_BLL.Interfaces;
using CDS_Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CDS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _service;
        
        public LoginController(ILoginService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoginDTO>>> GetAll()
        {
            var logins = await _service.GetAllAsync();
            return Ok(logins);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LoginDTO>> GetById(int id)
        {
            var login = await _service.GetByIdAsync(id);
            if (login == null) return NotFound();
            return Ok(login);
        }

        [HttpGet("usuario/{usuario}")]
        public async Task<ActionResult<LoginDTO>> GetByUsuario(string usuario)
        {
            var login = await _service.GetByUsuarioAsync(usuario);
            if (login == null) return NotFound();
            return Ok(login);
        }

        [HttpPost("validate")]
        public async Task<ActionResult<LoginDTO>> ValidateLogin([FromBody] LoginValidationRequest request)
        {
            var login = await _service.ValidateLoginAsync(request.Usuario, request.Password);
            if (login == null) return Unauthorized();
            return Ok(login);
        }

        [HttpPost]
        public async Task<ActionResult<LoginDTO>> Create(LoginDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.IdLogin }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, LoginDTO dto)
        {
            if (id != dto.IdLogin) return BadRequest();
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

    public class LoginValidationRequest
    {
        public string Usuario { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
} 