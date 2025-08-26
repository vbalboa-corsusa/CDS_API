using System.Collections.Generic;
using System.Threading.Tasks;
using CDS_Models.DTOs;

namespace CDS_BLL.Interfaces
{
    public interface ICatFormaPagoService
    {
        Task<IEnumerable<CatFormaPagoDTO>> GetAllAsync();
        Task<CatFormaPagoDTO?> GetByIdAsync(int id);
        Task<CatFormaPagoDTO> CreateAsync(CatFormaPagoDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(int id, CatFormaPagoDTO dto);
    }
}
