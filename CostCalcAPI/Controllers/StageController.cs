using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CostCalcAPI.Data;
using CostCalcAPI.Models;

namespace CostCalcAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StageController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StageController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Stage
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stage>>> GetStages()
        {
            return await _context.Stages.ToListAsync();
        }

        // POST: api/Stage
        [HttpPost]
        public async Task<ActionResult<Stage>> PostStage(Stage stage)
        {
            _context.Stages.Add(stage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStage", new { id = stage.Id }, stage);
        }
    }
}
