using BeerInventory.Models;
using BeerInventory.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerInventory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BeerController : ControllerBase
    {
        private readonly BeerService _beerService;

        public BeerController(BeerService beerService)
        {
            _beerService = beerService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Beer>> Get(int id)
        {
            var beer = await _beerService.GetBeerByIdAsync(id);
            if (beer == null) return NotFound();
            return Ok(beer);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Beer>>> GetBeers([FromQuery] decimal? gtAlcoholByVolume, [FromQuery] decimal? ltAlcoholByVolume)
        {
            var beers = await _beerService.GetBeersAsync(gtAlcoholByVolume, ltAlcoholByVolume);
            return Ok(beers);
        }

        [HttpPost]
        public async Task<IActionResult> InsertBeer([FromBody] Beer newBeer)
        {
            if (newBeer == null || string.IsNullOrWhiteSpace(newBeer.Name) || newBeer.PercentageAlcoholByVolume <= 0)
                return BadRequest("Invalid beer data.");

            var addedBeer = await _beerService.AddBeerAsync(newBeer);
            return CreatedAtAction(nameof(Get), new { id = addedBeer.Id }, addedBeer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBeer(int id, [FromBody] Beer updatedBeer)
        {
            if (updatedBeer == null || id != updatedBeer.Id)
                return BadRequest("Invalid beer data.");

            try
            {
                await _beerService.UpdateBeerAsync(id, updatedBeer);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
