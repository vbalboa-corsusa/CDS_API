using CDS_Models.DTOs;

namespace CDS_BLL.Interfaces
{
    public interface ILoginService
    {
        Task<IEnumerable<LoginDTO>> GetAllAsync();
        Task<LoginDTO?> GetByIdAsync(int id);
        Task<LoginDTO?> GetByUsuarioAsync(string usuario);
        Task<LoginDTO?> ValidateLoginAsync(string usuario, string password);
        Task<LoginDTO> CreateAsync(LoginDTO dto);
        Task<bool> UpdateAsync(int id, LoginDTO dto);
        Task<bool> DeleteAsync(int id);
    }
} 