using BeerInventory.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerInventory.Data.Repositories
{
    public class BarBeerRepository : IBarBeerRepository
    {
        private readonly BeerContext _context;

        public BarBeerRepository(BeerContext context)
        {
            _context = context;
        }

        public async Task<Bar> GetBarWithBeersAsync(int barId)
        {
            return await _context.Bars
                .Include(b => b.Beers)
                .FirstOrDefaultAsync(b => b.Id == barId);
        }

        public async Task<IEnumerable<Bar>> GetAllBarsWithBeersAsync()
        {
            return await _context.Bars
                .Include(b => b.Beers)
                .ToListAsync();
        }

        public async Task<bool> LinkBeerToBarAsync(int barId, int beerId)
        {
            var bar = await _context.Bars.Include(b => b.Beers).FirstOrDefaultAsync(b => b.Id == barId);
            var beer = await _context.Beers.FindAsync(beerId);

            if (bar == null || beer == null)
            {
                return false;
            }

            if (!bar.Beers.Contains(beer))
            {
                bar.Beers.Add(beer);
                await _context.SaveChangesAsync();
            }

            return true;
        }
    }
}
