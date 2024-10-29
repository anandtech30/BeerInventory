using BeerInventory.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerInventory.Data.Repositories
{
    public interface IBarRepository
    {
        Task<Bar> GetByIdAsync(int id);
        Task<IEnumerable<Bar>> GetAllBarsAsync();
        Task<Bar> AddBarAsync(Bar bar);
        Task UpdateBarAsync(Bar bar);
        Task<bool> BarExistsAsync(int id);
    }
}
