using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CostCalcAPI.Data;
using CostCalcAPI.Models;
using System.Linq;
using System.Threading.Tasks;

namespace CostCalcAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CostController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CostController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Cost/project/{projectId}
        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<decimal>> GetProjectCost(int projectId, bool includeOverhead = true)
        {
            // Получаем проект по ID
            var project = await _context.Projects
                .Include(p => p.Stages)
                    .ThenInclude(s => s.Roles)
                .Include(p => p.OverheadCosts)
                .FirstOrDefaultAsync(p => p.Id == projectId);

            if (project == null)
            {
                return NotFound();
            }

            // Расчет стоимости по этапам (по ролям на каждом этапе)
            decimal totalCost = 0;

            foreach (var stage in project.Stages)
            {
                foreach (var role in stage.Roles)
                {
                    // Стоимость по роли: зарплата * продолжительность этапа в неделях
                    totalCost += role.WeeklySalary * stage.DurationWeeks;
                }
            }

            // Добавляем накладные расходы, если нужно
            if (includeOverhead)
            {
                totalCost += project.OverheadCosts.Sum(oc => oc.Amount);
            }

            // Возвращаем итоговую стоимость
            return totalCost;
        }
        // GET: api/Cost/project/{projectId}/stages
        [HttpGet("project/{projectId}/stages")]
        public async Task<ActionResult<decimal>> GetCostForStages(int projectId, [FromQuery] List<int> stageIds, bool includeOverhead = false)
        {
            var project = await _context.Projects
                .Include(p => p.Stages)
                    .ThenInclude(s => s.Roles)
                .Include(p => p.OverheadCosts)
                .FirstOrDefaultAsync(p => p.Id == projectId);

            if (project == null)
            {
                return NotFound();
            }

            var stagesToCalculate = project.Stages.Where(stage => stageIds.Contains(stage.Id)).ToList();

            if (!stagesToCalculate.Any())
            {
                return BadRequest("None of the specified stages were found in the project.");
            }

            decimal totalCost = 0;

            foreach (var stage in stagesToCalculate)
            {
                foreach (var role in stage.Roles)
                {
                    totalCost += role.WeeklySalary * stage.DurationWeeks;
                }
            }

            if (includeOverhead)
            {
                totalCost += project.OverheadCosts.Sum(oc => oc.Amount);
            }

            return totalCost;
        }
    }
}
