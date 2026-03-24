using Microsoft.EntityFrameworkCore;
using NRC.Const.CodesAPI.Domain.Entities.Core;
using NRC.Const.CodesAPI.Domain.Entities.Workplanning.Tasks;

namespace NRC.Const.CodesAPI.Infrastructure.Persistence.DbContexts
{
    public class TasksDbContext : DbContext
    {
        public TasksDbContext(DbContextOptions<TasksDbContext> options) : base(options) { }
        public DbSet<CodesTask> CodesTask { get; set; }
        public DbSet<CodesTaskActivities> CodesTasksActivities {  get; set; }
        public DbSet<CodesTaskCriticality> CodesTaskCriticality { get; set; }
        public DbSet<CodesTaskPCFs> CodesTaskPCFs { get; set; }
        public DbSet<CodesTaskResource> CodesTaskResource { get; set; }
        public DbSet<CodesTaskCodeBook> CodesTasksCodeBook { get; set; }
        public DbSet<CodesTaskStatus> CodesTaskStatus { get; set; }
        public DbSet<CodesStrategicPriority> CodesStrategicPriority { get; set; }
        public DbSet<CodesLifecycleStage> CodesLifecycleStage { get; set; }

        public DbSet<CodesTaskDependencies> CodesTaskDependencies { get; set; }
        public DbSet<CodesTaskType> CodesTaskType { get; set; }
        public DbSet<CodesCycle> CodesCycle { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TasksDbContext).Assembly);


        }
    }
}

