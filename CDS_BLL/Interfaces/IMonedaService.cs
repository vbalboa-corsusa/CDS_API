using System.Collections.Generic;
using System.Threading.Tasks;
using CDS_Models.DTOs;

namespace CDS_BLL.Interfaces
{
    public interface IMonedaService
    {
        Task<IEnumerable<MonedaDTO>> GetAllAsync();
    }
}
