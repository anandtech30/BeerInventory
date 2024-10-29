using BeerInventory.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerInventory.Data.Repositories
{
    public class BreweryBeerRepository : IBreweryBeerRepository
    {
        private readonly BeerContext _context;

        public BreweryBeerRepository(BeerContext context)
        {
            _context = context;
        }

        public async Task<Brewery> GetBreweryWithBeersAsync(int breweryId)
        {
            return await _context.Breweries
                .Include(b => b.Beers)
                .FirstOrDefaultAsync(b => b.Id == breweryId);
        }

        public async Task<IEnumerable<Brewery>> GetAllBreweriesWithBeersAsync()
        {
            return await _context.Breweries
                .Include(b => b.Beers)
                .ToListAsync();
        }

        public async Task<Beer> AddBeerToBreweryAsync(Beer beer)
        {
            var brewery = await _context.Breweries.FindAsync(beer.BreweryId);
            if (brewery == null)
            {
                return null;
            }

            await _context.Beers.AddAsync(beer);
            await _context.SaveChangesAsync();
            return beer;
        }
    }
}
