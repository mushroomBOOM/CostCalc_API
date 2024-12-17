using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CostCalcAPI.Data;
using CostCalcAPI.Models;

namespace CostCalcAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjectController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Project
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            return await _context.Projects.Include(p => p.Stages)
                                          .ThenInclude(s => s.Roles)
                                          .Include(p => p.OverheadCosts)
                                          .ToListAsync();
        }

        // GET: api/Project/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            var project = await _context.Projects
                                         .Include(p => p.Stages)
                                         .ThenInclude(s => s.Roles)
                                         .Include(p => p.OverheadCosts)
                                         .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }

        // POST: api/Project
        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(Project project)
        {
            // Добавляем стадию и роли для проекта
            foreach (var stage in project.Stages)
            {
                foreach (var role in stage.Roles)
                {
                    // Проверяем, существует ли роль в базе данных
                    var existingRole = await _context.Roles
                                                      .FirstOrDefaultAsync(r => r.Id == role.Id);
                    if (existingRole == null)
                    {
                        _context.Roles.Add(role);  // Добавляем новую роль
                    }
                    else
                    {
                        _context.Entry(role).State = EntityState.Modified; // Обновляем роль
                    }
                }

                _context.Stages.Add(stage);  // Добавляем стадию
            }

            _context.Projects.Add(project);  // Добавляем проект

            // Сохраняем изменения в базе данных
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }

        // PUT: api/Project/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, Project project)
        {
            if (id != project.Id)
            {
                return BadRequest();
            }

            _context.Entry(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
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

        // DELETE: api/Project/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
