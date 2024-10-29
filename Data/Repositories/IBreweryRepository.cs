using BeerInventory.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerInventory.Data.Repositories
{
    public interface IBreweryRepository
    {
        Task<Brewery> GetByIdAsync(int id);
        Task<IEnumerable<Brewery>> GetAllBreweriesAsync();
        Task<Brewery> AddBreweryAsync(Brewery brewery);
        Task UpdateBreweryAsync(Brewery brewery);
        Task<bool> BreweryExistsAsync(int id);
    }
}
