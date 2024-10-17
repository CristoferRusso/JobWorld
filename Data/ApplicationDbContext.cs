using Microsoft.EntityFrameworkCore;
using JobWorld.Models;

namespace JobWorld.Data // Cambia YourNamespace con il tuo namespace
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Interview> Interviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Job>()
                .Property(j => j.Title)
                .IsRequired()
                .HasMaxLength(100);
                
        }
    }
}
