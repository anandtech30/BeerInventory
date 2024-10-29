using BeerInventory.Models;
using BeerInventory.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerInventory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BarBeerController : ControllerBase
    {
        private readonly BarBeerService _barBeerService;

        public BarBeerController(BarBeerService barBeerService)
        {
            _barBeerService = barBeerService;
        }

        [HttpPost("beer")]
        public async Task<IActionResult> LinkBeerToBar(int barId, int beerId)
        {
            try
            {
                await _barBeerService.LinkBeerToBarAsync(barId, beerId);
                return Ok("Beer linked to bar successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{barId}/beer")]
        public async Task<ActionResult<Bar>> GetBarWithBeers(int barId)
        {
            try
            {
                var bar = await _barBeerService.GetBarWithBeersAsync(barId);
                return Ok(bar);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("beer")]
        public async Task<ActionResult<IEnumerable<Bar>>> GetAllBarsWithBeers()
        {
            var bars = await _barBeerService.GetAllBarsWithBeersAsync();
            return Ok(bars);
        }
    }
}
