using System.Collections.Generic;
using System.Threading.Tasks;
using CDS_Models.DTOs;

namespace CDS_BLL.Interfaces
{
    public interface IVendedorService
    {
        Task<IEnumerable<VendedorDTO>> GetAllAsync();
        Task<VendedorDTO?> GetByIdAsync(string id);
        Task<VendedorDTO> CreateAsync(VendedorDTO dto);
        Task<bool> UpdateAsync(string id, VendedorDTO dto);
        Task<bool> DeleteAsync(string id);
    }
} 