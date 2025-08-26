using System.Collections.Generic;
using System.Threading.Tasks;
using CDS_Models.DTOs;

namespace CDS_BLL.Interfaces
{
    public interface IStatusOpService
    {
        Task<IEnumerable<EstadosOpDTO>> GetAllAsync();
        Task<EstadosOpDTO?> GetByIdAsync(int id);
        Task<EstadosOpDTO> CreateAsync(EstadosOpDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(int id, EstadosOpDTO dto);
    }
}
