using System.Collections.Generic;
using System.Threading.Tasks;
using CDS_Models.DTOs;

namespace CDS_BLL.Interfaces
{
    public interface IProductoService
    {
        Task<IEnumerable<ProductoDTO>> GetAllAsync();
        Task<ProductoDTO?> GetByIdAsync(int id);
        Task<ProductoDTO> CreateAsync(ProductoDTO dto);
        Task<bool> UpdateAsync(int id, ProductoDTO dto);
        Task<bool> DeleteAsync(int id);
    }
} 