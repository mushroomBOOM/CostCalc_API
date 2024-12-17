using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CostCalcAPI.Data;
using CostCalcAPI.Models;

namespace CostCalcAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OverheadCostController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OverheadCostController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/OverheadCost
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OverheadCost>>> GetOverheadCosts()
        {
            return await _context.OverheadCosts.ToListAsync();
        }

        // GET: api/OverheadCost/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<OverheadCost>> GetOverheadCost(int id)
        {
            var overheadCost = await _context.OverheadCosts.FindAsync(id);

            if (overheadCost == null)
            {
                return NotFound();
            }

            return overheadCost;
        }

        // POST: api/OverheadCost
        [HttpPost]
        public async Task<ActionResult<OverheadCost>> PostOverheadCost(OverheadCost overheadCost)
        {
            _context.OverheadCosts.Add(overheadCost);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOverheadCost", new { id = overheadCost.Id }, overheadCost);
        }

        // PUT: api/OverheadCost/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOverheadCost(int id, OverheadCost overheadCost)
        {
            if (id != overheadCost.Id)
            {
                return BadRequest();
            }

            _context.Entry(overheadCost).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OverheadCostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/OverheadCost/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOverheadCost(int id)
        {
            var overheadCost = await _context.OverheadCosts.FindAsync(id);
            if (overheadCost == null)
            {
                return NotFound();
            }

            _context.OverheadCosts.Remove(overheadCost);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OverheadCostExists(int id)
        {
            return _context.OverheadCosts.Any(e => e.Id == id);
        }
    }
}
