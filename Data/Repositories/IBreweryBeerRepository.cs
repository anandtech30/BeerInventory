using BeerInventory.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerInventory.Data.Repositories
{
    public interface IBreweryBeerRepository
    {
        Task<Brewery> GetBreweryWithBeersAsync(int breweryId);
        Task<IEnumerable<Brewery>> GetAllBreweriesWithBeersAsync();
        Task<Beer> AddBeerToBreweryAsync(Beer beer);
    }
}
