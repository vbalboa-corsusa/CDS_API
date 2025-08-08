using CDS_Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CDS_BLL.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDTO>> GetAllAsync();
        Task<ClienteDTO?> GetByIdAsync(string id);
        Task<ClienteDTO> CreateAsync(ClienteDTO dto);
        Task<bool> DeleteAsync(string id);
        Task<bool> UpdateAsync(string id, ClienteDTO dto);
    }
}