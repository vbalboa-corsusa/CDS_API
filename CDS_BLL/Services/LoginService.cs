using CDS_BLL.Interfaces;
using CDS_Models.DTOs;
using CDS_DAL;
using Microsoft.EntityFrameworkCore;

namespace CDS_BLL.Services
{
    public class LoginService : ILoginService
    {
        private readonly LogistContext _context;

        public LoginService(LogistContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LoginDTO>> GetAllAsync()
        {
            var logins = await _context.Logins
                .Where(l => l.Estado == true)
                .Select(l => new LoginDTO
                {
                    IdLogin = l.IdLogin,
                    Usuario = l.Usuario ?? string.Empty,
                    Pass = l.Pass ?? string.Empty,
                    Estado = l.Estado ?? false
                })
                .ToListAsync();

            return logins;
        }

        public async Task<LoginDTO?> GetByIdAsync(int id)
        {
            var login = await _context.Logins
                .Where(l => l.IdLogin == id && l.Estado == true)
                .Select(l => new LoginDTO
                {
                    IdLogin = l.IdLogin,
                    Usuario = l.Usuario ?? string.Empty,
                    Pass = l.Pass ?? string.Empty,
                    Estado = l.Estado ?? false
                })
                .FirstOrDefaultAsync();

            return login;
        }

        public async Task<LoginDTO?> GetByUsuarioAsync(string usuario)
        {
            var login = await _context.Logins
                .Where(l => l.Usuario == usuario && l.Estado == true)
                .Select(l => new LoginDTO
                {
                    IdLogin = l.IdLogin,
                    Usuario = l.Usuario ?? string.Empty,
                    Pass = l.Pass ?? string.Empty,
                    Estado = l.Estado ?? false
                })
                .FirstOrDefaultAsync();

            return login;
        }

        public async Task<LoginDTO?> ValidateLoginAsync(string usuario, string password)
        {
            var login = await _context.Logins
                .Where(l => l.Usuario == usuario && l.Pass == password && l.Estado == true)
                .Select(l => new LoginDTO
                {
                    IdLogin = l.IdLogin,
                    Usuario = l.Usuario ?? string.Empty,
                    Pass = l.Pass ?? string.Empty,
                    Estado = l.Estado ?? false
                })
                .FirstOrDefaultAsync();

            return login;
        }

        public async Task<LoginDTO> CreateAsync(LoginDTO dto)
        {
            // Obtener el máximo IdLogin existente (incluyendo eliminados lógicamente)
            int maxId = 0;
            if (await _context.Logins.AnyAsync())
            {
                maxId = await _context.Logins.MaxAsync(l => l.IdLogin);
            }
            var login = new CDS_Models.Login
            {
                IdLogin = maxId + 1, // Asignar el nuevo Id manualmente
                Usuario = dto.Usuario,
                Pass = dto.Pass,
                Estado = dto.Estado
            };

            _context.Logins.Add(login);
            await _context.SaveChangesAsync();

            dto.IdLogin = login.IdLogin;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, LoginDTO dto)
        {
            var login = await _context.Logins.FindAsync(id);
            if (login == null) return false;

            login.Usuario = dto.Usuario;
            login.Pass = dto.Pass;
            login.Estado = dto.Estado;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var login = await _context.Logins.FindAsync(id);
            if (login == null) return false;

            login.Estado = false; // Soft delete
            await _context.SaveChangesAsync();
            return true;
        }
    }
} 