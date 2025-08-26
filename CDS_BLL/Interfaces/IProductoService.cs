using System.Collections.Generic;
using System.Threading.Tasks;
using CDS_Models.DTOs;

namespace CDS_BLL.Interfaces
{
    public interface IProductoService
    {
        Task<IEnumerable<ProductoDTO>> GetAllAsync();
        Task<ProductoDTO?> GetByIdAsync(string id);
        Task<ProductoDTO> CreateAsync(ProductoDTO dto);
        Task<bool> UpdateAsync(string id, ProductoDTO dto);
        Task<bool> DeleteAsync(string id);
    }
} 