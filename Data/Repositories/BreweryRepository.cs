using BeerInventory.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerInventory.Data.Repositories
{
    public class BreweryRepository : IBreweryRepository
    {
        private readonly BeerContext _context;

        public BreweryRepository(BeerContext context)
        {
            _context = context;
        }

        public async Task<Brewery> GetByIdAsync(int id)
        {
            return await _context.Breweries.FindAsync(id);
        }

        public async Task<IEnumerable<Brewery>> GetAllBreweriesAsync()
        {
            return await _context.Breweries.ToListAsync();
        }

        public async Task<Brewery> AddBreweryAsync(Brewery brewery)
        {
            await _context.Breweries.AddAsync(brewery);
            await _context.SaveChangesAsync();
            return brewery;
        }

        public async Task UpdateBreweryAsync(Brewery brewery)
        {
            _context.Entry(brewery).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> BreweryExistsAsync(int id)
        {
            return await _context.Breweries.AnyAsync(b => b.Id == id);
        }
    }
}
