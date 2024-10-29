using BeerInventory.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerInventory.Data.Repositories
{
    public class BarRepository : IBarRepository
    {
        private readonly BeerContext _context;

        public BarRepository(BeerContext context)
        {
            _context = context;
        }

        public async Task<Bar> GetByIdAsync(int id)
        {
            return await _context.Bars.FindAsync(id);
        }

        public async Task<IEnumerable<Bar>> GetAllBarsAsync()
        {
            return await _context.Bars.ToListAsync();
        }

        public async Task<Bar> AddBarAsync(Bar bar)
        {
            await _context.Bars.AddAsync(bar);
            await _context.SaveChangesAsync();
            return bar;
        }

        public async Task UpdateBarAsync(Bar bar)
        {
            _context.Entry(bar).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> BarExistsAsync(int id)
        {
            return await _context.Bars.AnyAsync(b => b.Id == id);
        }
    }
}
