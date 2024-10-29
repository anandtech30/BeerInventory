using BeerInventory.Data.Repositories;
using BeerInventory.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerInventory.Services
{
    public class BeerService
    {
        private readonly IBeerRepository _beerRepository;

        public BeerService(IBeerRepository beerRepository)
        {
            _beerRepository = beerRepository;
        }

        public Task<Beer> GetBeerByIdAsync(int id)
        {
            return _beerRepository.GetByIdAsync(id);
        }

        public Task<IEnumerable<Beer>> GetBeersAsync(decimal? gtAlcoholByVolume, decimal? ltAlcoholByVolume)
        {
            return _beerRepository.GetBeersAsync(gtAlcoholByVolume, ltAlcoholByVolume);
        }

        public Task<Beer> AddBeerAsync(Beer newBeer)
        {
            return _beerRepository.AddBeerAsync(newBeer);
        }

        public async Task UpdateBeerAsync(int id, Beer updatedBeer)
        {
            var existingBeer = await _beerRepository.GetByIdAsync(id);
            if (existingBeer == null) throw new KeyNotFoundException("Beer not found.");

            existingBeer.Name = updatedBeer.Name;
            existingBeer.PercentageAlcoholByVolume = updatedBeer.PercentageAlcoholByVolume;
            await _beerRepository.UpdateBeerAsync(existingBeer);
        }
    }
}
