using BeerInventory.Models;
using BeerInventory.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerInventory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BarController : ControllerBase
    {
        private readonly BarService _barService;

        public BarController(BarService barService)
        {
            _barService = barService;
        }

        [HttpPost]
        public async Task<ActionResult<Bar>> CreateBar([FromBody] Bar bar)
        {
            var createdBar = await _barService.AddBarAsync(bar);
            return CreatedAtAction(nameof(GetBarById), new { id = createdBar.Id }, createdBar);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBar(int id, [FromBody] Bar bar)
        {
            try
            {
                await _barService.UpdateBarAsync(id, bar);
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
        public async Task<ActionResult<IEnumerable<Bar>>> GetAllBars()
        {
            var bars = await _barService.GetAllBarsAsync();
            return Ok(bars);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Bar>> GetBarById(int id)
        {
            var bar = await _barService.GetBarByIdAsync(id);
            if (bar == null)
                return NotFound();

            return Ok(bar);
        }
    }
}
