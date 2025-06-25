using System.Collections.Generic;
using System.Threading.Tasks;
using CDS_Models.DTOs;

namespace CDS_BLL.Interfaces
{
    public interface IVendedorService
    {
        Task<IEnumerable<VendedorDTO>> GetAllAsync();
        Task<VendedorDTO?> GetByIdAsync(int id);
        Task<VendedorDTO> CreateAsync(VendedorDTO dto);
        Task<bool> UpdateAsync(int id, VendedorDTO dto);
        Task<bool> DeleteAsync(int id);
    }
} 