using BeerInventory.Data.Repositories;
using BeerInventory.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerInventory.Services
{
    public class BreweryBeerService
    {
        private readonly IBreweryBeerRepository _breweryBeerRepository;

        public BreweryBeerService(IBreweryBeerRepository breweryBeerRepository)
        {
            _breweryBeerRepository = breweryBeerRepository;
        }

        public async Task<Brewery> GetBreweryWithBeersAsync(int breweryId)
        {
            var brewery = await _breweryBeerRepository.GetBreweryWithBeersAsync(breweryId);
            if (brewery == null)
                throw new KeyNotFoundException("Brewery not found.");

            return brewery;
        }

        public Task<IEnumerable<Brewery>> GetAllBreweriesWithBeersAsync()
        {
            return _breweryBeerRepository.GetAllBreweriesWithBeersAsync();
        }

        public async Task<Beer> AddBeerToBreweryAsync(Beer beer)
        {
            var addedBeer = await _breweryBeerRepository.AddBeerToBreweryAsync(beer);
            if (addedBeer == null)
                throw new KeyNotFoundException("Brewery not found.");

            return addedBeer;
        }
    }
}
