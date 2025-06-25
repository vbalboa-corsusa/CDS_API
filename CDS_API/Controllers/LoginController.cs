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
    [SwaggerTag("Gesti�n de usuario con inicio de sesi�n")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _service;
        
        public LoginController(ILoginService service)
        {
            _service = service;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtiene todos los usuarios")]
        [SwaggerResponse(200, "Lista de usuarios obtenida exitosamente", typeof(IEnumerable<LoginDTO>))]
        [SwaggerResponse(404, "No se encontraron los usuarios de sesi�n")]
        public async Task<ActionResult<IEnumerable<LoginDTO>>> GetAll()
        {
            var logins = await _service.GetAllAsync();
            return Ok(logins);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtiene un usuario por ID")]
        [SwaggerResponse(200, "Usuario encontrado", typeof(LoginDTO))]
        [SwaggerResponse(404, "Usuario no encontrado")]
        public async Task<ActionResult<LoginDTO>> GetById(int id)
        {
            var login = await _service.GetByIdAsync(id);
            if (login == null) return NotFound();
            return Ok(login);
        }

        [HttpGet("usuario/{usuario}")]
        [SwaggerOperation(Summary = "Obtiene un inicio de sesi�n por usuario")]
        [SwaggerResponse(200, "Inicio de sesi�n encontrado", typeof(LoginDTO))]
        [SwaggerResponse(404, "Inicio de sesi�n no encontrado")]
        public async Task<ActionResult<LoginDTO>> GetByUsuario(string usuario)
        {
            var login = await _service.GetByUsuarioAsync(usuario);
            if (login == null) return NotFound();
            return Ok(login);
        }

        [HttpPost("validate")]
        [SwaggerOperation(Summary = "Valida un inicio de sesi�n")]
        [SwaggerResponse(200, "Inicio de sesi�n validado exitosamente", typeof(LoginDTO))]
        [SwaggerResponse(401, "Credenciales inv�lidas")]
        public async Task<ActionResult<LoginDTO>> ValidateLogin([FromBody] LoginValidationRequest request)
        {
            var login = await _service.ValidateLoginAsync(request.Usuario, request.Password);
            if (login == null) return Unauthorized();
            return Ok(login);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Crea un nuevo inicio de sesi�n")]
        [SwaggerResponse(201, "Inicio de sesi�n creado", typeof(LoginDTO))]
        [SwaggerResponse(400, "Datos inv�lidos")]
        public async Task<ActionResult<LoginDTO>> Create(LoginDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.IdLogin }, created);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Actualiza un inicio de sesi�n existente")]
        [SwaggerResponse(204, "Inicio de sesi�n actualizado")]
        [SwaggerResponse(400, "ID no coincide")]
        [SwaggerResponse(404, "Inicio de sesi�n no encontrado")]
        public async Task<IActionResult> Update(int id, LoginDTO dto)
        {
            if (id != dto.IdLogin) return BadRequest();
            var ok = await _service.UpdateAsync(id, dto);
            if (!ok) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Elimina un inicio de sesi�n")]
        [SwaggerResponse(204, "Inicio de sesi�n eliminado")]
        [SwaggerResponse(404, "Inicio de sesi�n no encontrado")]
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