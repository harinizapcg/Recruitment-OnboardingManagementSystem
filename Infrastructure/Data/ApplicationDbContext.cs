using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Add your DbSets here
        // Example: public DbSet<YourEntity> YourEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure your entities here
            // Example:
            // modelBuilder.Entity<YourEntity>(entity =>
            // {
            //     entity.HasKey(e => e.Id);
            //     entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            // });
        }
    }
}
