using BeerInventory.Models;
using BeerInventory.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerInventory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BreweryController : ControllerBase
    {
        private readonly BreweryService _breweryService;

        public BreweryController(BreweryService breweryService)
        {
            _breweryService = breweryService;
        }

        [HttpPost]
        public async Task<ActionResult<Brewery>> CreateBrewery([FromBody] Brewery brewery)
        {
            var createdBrewery = await _breweryService.AddBreweryAsync(brewery);
            return CreatedAtAction(nameof(GetBreweryById), new { id = createdBrewery.Id }, createdBrewery);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBrewery(int id, [FromBody] Brewery brewery)
        {
            try
            {
                await _breweryService.UpdateBreweryAsync(id, brewery);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brewery>>> GetAllBreweries()
        {
            var breweries = await _breweryService.GetAllBreweriesAsync();
            return Ok(breweries);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Brewery>> GetBreweryById(int id)
        {
            var brewery = await _breweryService.GetBreweryByIdAsync(id);
            if (brewery == null)
                return NotFound();

            return Ok(brewery);
        }
    }
}
