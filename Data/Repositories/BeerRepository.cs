using BeerInventory.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerInventory.Data.Repositories
{
    public class BeerRepository : IBeerRepository
    {
        private readonly BeerContext _context;

        public BeerRepository(BeerContext context)
        {
            _context = context;
        }

        public async Task<Beer> GetByIdAsync(int id)
        {
            return await _context.Beers.FindAsync(id);
        }

        public async Task<IEnumerable<Beer>> GetBeersAsync(decimal? gtAlcoholByVolume, decimal? ltAlcoholByVolume)
        {
            var query = _context.Beers.AsQueryable();

            if (gtAlcoholByVolume.HasValue)
                query = query.Where(b => b.PercentageAlcoholByVolume > gtAlcoholByVolume.Value);

            if (ltAlcoholByVolume.HasValue)
                query = query.Where(b => b.PercentageAlcoholByVolume < ltAlcoholByVolume.Value);

            return await query.ToListAsync();
        }

        public async Task<Beer> AddBeerAsync(Beer newBeer)
        {
            await _context.Beers.AddAsync(newBeer);
            await _context.SaveChangesAsync();
            return newBeer;
        }

        public async Task UpdateBeerAsync(Beer beer)
        {
            _context.Entry(beer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
