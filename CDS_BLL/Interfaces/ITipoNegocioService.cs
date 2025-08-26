using System.Collections.Generic;
using System.Threading.Tasks;
using CDS_Models.DTOs;

namespace CDS_BLL.Interfaces
{
    public interface ITipoNegocioService
    {
        Task<IEnumerable<TipoNegocioDTO>> GetAllAsync();
        Task<TipoNegocioDTO?> GetByIdAsync(int id);
        Task<TipoNegocioDTO> CreateAsync(TipoNegocioDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(int id, TipoNegocioDTO dto);
    }
}
