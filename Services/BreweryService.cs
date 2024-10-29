using BeerInventory.Data.Repositories;
using BeerInventory.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerInventory.Services
{
    public class BreweryService
    {
        private readonly IBreweryRepository _breweryRepository;

        public BreweryService(IBreweryRepository breweryRepository)
        {
            _breweryRepository = breweryRepository;
        }

        public Task<Brewery> GetBreweryByIdAsync(int id)
        {
            return _breweryRepository.GetByIdAsync(id);
        }

        public Task<IEnumerable<Brewery>> GetAllBreweriesAsync()
        {
            return _breweryRepository.GetAllBreweriesAsync();
        }

        public Task<Brewery> AddBreweryAsync(Brewery brewery)
        {
            return _breweryRepository.AddBreweryAsync(brewery);
        }

        public async Task UpdateBreweryAsync(int id, Brewery updatedBrewery)
        {
            if (id != updatedBrewery.Id)
                throw new ArgumentException("Brewery ID mismatch");

            var existingBrewery = await _breweryRepository.GetByIdAsync(id);
            if (existingBrewery == null)
                throw new KeyNotFoundException("Brewery not found");

            existingBrewery.Name = updatedBrewery.Name;

            await _breweryRepository.UpdateBreweryAsync(existingBrewery);
        }

        public Task<bool> BreweryExistsAsync(int id)
        {
            return _breweryRepository.BreweryExistsAsync(id);
        }
    }
}
