using English.Class.Domain.Groups;
using English.Class.Domain.Homeworks;
using English.Class.Domain.Schedules;
using English.Class.Domain.Students;
using Microsoft.EntityFrameworkCore;

namespace English.Class.Infrastructure.Database
{
    public sealed class AppDbContext : DbContext
    {
        public DbSet<Group> Group { get; set; } = null!;
        public DbSet<Homework> Homework { get; set; } = null!;
        public DbSet<Schedule> Schedule { get; set; } = null!;
        public DbSet<Student> Student { get; set; } = null!;

        public AppDbContext(DbContextOptions options) : base(options)
        {
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}