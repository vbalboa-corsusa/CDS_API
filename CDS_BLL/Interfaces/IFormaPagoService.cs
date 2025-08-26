using System.Collections.Generic;
using System.Threading.Tasks;
using CDS_Models.DTOs;

namespace CDS_BLL.Interfaces
{
    public interface IFormaPagoService
    {
        Task<IEnumerable<FormaPagoDTO>> GetAllAsync();
        Task<FormaPagoDTO?> GetByIdAsync(int id);
        Task<FormaPagoDTO> CreateAsync(FormaPagoDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(int id, FormaPagoDTO dto);
    }
}
