using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Requisition> Requisitions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Interview> Interviews { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<JobApplication> JobApplications { get; set; }

    public DbSet<InterviewFeedback> InterviewFeedbacks { get; set; }

    public DbSet<Offer> Offers { get; set; }

    public DbSet<Onboarding> Onboardings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Offer>()
            .Property(o => o.Salary)
            .HasPrecision(18, 2); // ✅ FIX
    }


    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            throw new Exception(ex.InnerException?.Message ?? ex.Message);
        }
    }
}