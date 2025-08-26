using System.Collections.Generic;
using System.Threading.Tasks;
using CDS_Models.DTOs;

namespace CDS_BLL.Interfaces
{
    public interface ITipoDocumentoService
    {
        Task<IEnumerable<TipoDocsIdentDTO>> GetAllAsync();
        Task<TipoDocsIdentDTO?> GetByIdAsync(int id);
        Task<TipoDocsIdentDTO> CreateAsync(TipoDocsIdentDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(int id, TipoDocsIdentDTO dto);
    }
}
