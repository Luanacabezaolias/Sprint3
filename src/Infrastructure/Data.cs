using Challenge_sprint.src.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Challenge_sprint.src.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSets das entidades
        public DbSet<User> Users { get; set; }
        public DbSet<SelfAssessment> SelfAssessments { get; set; }
        public DbSet<JournalEntry> JournalEntries { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Alert> Alerts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações de relacionamento
            modelBuilder.Entity<User>()
                .HasMany(u => u.Assessments)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.JournalEntries)
                .WithOne(j => j.User)
                .HasForeignKey(j => j.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Goals)
                .WithOne(g => g.User)
                .HasForeignKey(g => g.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Alerts)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);
        }
    }
}
