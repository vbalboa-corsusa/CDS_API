using System.Collections.Generic;
using System.Threading.Tasks;
using CDS_Models.DTOs;

namespace CDS_BLL.Interfaces
{
    public interface ISubSubTipoNegocioService
    {
        Task<IEnumerable<SubSubTipoNegocioDTO>> GetAllAsync();
        Task<SubSubTipoNegocioDTO?> GetByIdAsync(int id);
        Task<SubSubTipoNegocioDTO> CreateAsync(SubSubTipoNegocioDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(int id, SubSubTipoNegocioDTO dto);
    }
}
