using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

        public DbSet<UserTask> UserTasks { get; set; }
        public DbSet<TaskAssignment> TaskAssignments { get; set; }
        public DbSet<TaskEvaluation> TaskEvaluations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
