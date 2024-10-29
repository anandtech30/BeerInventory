using BeerInventory.Data.Repositories;
using BeerInventory.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerInventory.Services
{
    public class BarBeerService
    {
        private readonly IBarBeerRepository _barBeerRepository;

        public BarBeerService(IBarBeerRepository barBeerRepository)
        {
            _barBeerRepository = barBeerRepository;
        }

        public async Task<Bar> GetBarWithBeersAsync(int barId)
        {
            var bar = await _barBeerRepository.GetBarWithBeersAsync(barId);
            if (bar == null)
                throw new KeyNotFoundException("Bar not found.");

            return bar;
        }

        public Task<IEnumerable<Bar>> GetAllBarsWithBeersAsync()
        {
            return _barBeerRepository.GetAllBarsWithBeersAsync();
        }

        public async Task LinkBeerToBarAsync(int barId, int beerId)
        {
            var linked = await _barBeerRepository.LinkBeerToBarAsync(barId, beerId);
            if (!linked)
                throw new KeyNotFoundException("Bar or Beer not found.");
        }
    }
}
