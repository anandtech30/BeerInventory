using BeerInventory.Models;
using Microsoft.EntityFrameworkCore;

namespace BeerInventory.Data
{
    public class BeerContext : DbContext
    {
        public DbSet<Beer> Beers { get; set; }
        public DbSet<Brewery> Breweries { get; set; }
        public DbSet<Bar> Bars { get; set; }

        public BeerContext(DbContextOptions<BeerContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Beer>().Property(b => b.PercentageAlcoholByVolume).HasPrecision(5, 2);
            modelBuilder.Entity<Beer>().HasMany(b => b.Bars)
                .WithMany(b => b.Beers)
                .UsingEntity(b => b.ToTable("BarBeers"));
            base.OnModelCreating(modelBuilder);
        }
    }
}
