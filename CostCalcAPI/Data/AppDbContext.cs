using CostCalcAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CostCalcAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<OverheadCost> OverheadCosts { get; set; }
    }
}
