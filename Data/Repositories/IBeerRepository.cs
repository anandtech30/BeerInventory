using BeerInventory.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerInventory.Data.Repositories
{
    public interface IBeerRepository
    {
        Task<Beer> GetByIdAsync(int id);
        Task<IEnumerable<Beer>> GetBeersAsync(decimal? gtAlcoholByVolume, decimal? ltAlcoholByVolume);
        Task<Beer> AddBeerAsync(Beer newBeer);
        Task UpdateBeerAsync(Beer beer);
    }
}
