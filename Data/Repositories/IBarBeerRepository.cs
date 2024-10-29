using BeerInventory.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerInventory.Data.Repositories
{
    public interface IBarBeerRepository
    {
        Task<Bar> GetBarWithBeersAsync(int barId);
        Task<IEnumerable<Bar>> GetAllBarsWithBeersAsync();
        Task<bool> LinkBeerToBarAsync(int barId, int beerId);
    }
}
