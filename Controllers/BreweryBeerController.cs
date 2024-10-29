using BeerInventory.Models;
using BeerInventory.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerInventory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BreweryBeerController : ControllerBase
    {
        private readonly BreweryBeerService _breweryBeerService;

        public BreweryBeerController(BreweryBeerService breweryBeerService)
        {
            _breweryBeerService = breweryBeerService;
        }

        [HttpPost("beer")]
        public async Task<ActionResult<Beer>> AddBeerToBrewery([FromBody] Beer beer)
        {
            try
            {
                var addedBeer = await _breweryBeerService.AddBeerToBreweryAsync(beer);
                return CreatedAtAction(nameof(GetBreweryWithBeers), new { breweryId = beer.BreweryId }, addedBeer);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{breweryId}/beer")]
        public async Task<ActionResult<Brewery>> GetBreweryWithBeers(int breweryId)
        {
            try
            {
                var brewery = await _breweryBeerService.GetBreweryWithBeersAsync(breweryId);
                return Ok(brewery);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("beer")]
        public async Task<ActionResult<IEnumerable<Brewery>>> GetAllBreweriesWithBeers()
        {
            var breweries = await _breweryBeerService.GetAllBreweriesWithBeersAsync();
            return Ok(breweries);
        }
    }
}
