using System.Collections.Generic;
using System.Threading.Tasks;
using CDS_Models.DTOs;

namespace CDS_BLL.Interfaces
{
    public interface IMarcaService
    {
        Task<IEnumerable<MarcaDTO>> GetAllAsync();
        Task<MarcaDTO?> GetByIdAsync(int id);
        Task<MarcaDTO> CreateAsync(MarcaDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(int id, MarcaDTO dto);
    }
}
