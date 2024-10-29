using BeerInventory.Data.Repositories;
using BeerInventory.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerInventory.Services
{
    public class BarService
    {
        private readonly IBarRepository _barRepository;

        public BarService(IBarRepository barRepository)
        {
            _barRepository = barRepository;
        }

        public Task<Bar> GetBarByIdAsync(int id)
        {
            return _barRepository.GetByIdAsync(id);
        }

        public Task<IEnumerable<Bar>> GetAllBarsAsync()
        {
            return _barRepository.GetAllBarsAsync();
        }

        public Task<Bar> AddBarAsync(Bar bar)
        {
            return _barRepository.AddBarAsync(bar);
        }

        public async Task UpdateBarAsync(int id, Bar updatedBar)
        {
            if (id != updatedBar.Id)
                throw new ArgumentException("Bar ID mismatch");

            var existingBar = await _barRepository.GetByIdAsync(id);
            if (existingBar == null)
                throw new KeyNotFoundException("Bar not found");

            existingBar.Name = updatedBar.Name;
            existingBar.Address = updatedBar.Address;

            await _barRepository.UpdateBarAsync(existingBar);
        }

        public Task<bool> BarExistsAsync(int id)
        {
            return _barRepository.BarExistsAsync(id);
        }
    }
}
