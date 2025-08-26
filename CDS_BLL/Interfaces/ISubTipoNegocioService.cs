using System.Collections.Generic;
using System.Threading.Tasks;
using CDS_Models.DTOs;

namespace CDS_BLL.Interfaces
{
    public interface ISubTipoNegocioService
    {
        Task<IEnumerable<SubTipoNegocioDTO>> GetAllAsync();
        Task<SubTipoNegocioDTO?> GetByIdAsync(int id);
        Task<SubTipoNegocioDTO> CreateAsync(SubTipoNegocioDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(int id, SubTipoNegocioDTO dto);
    }
}
