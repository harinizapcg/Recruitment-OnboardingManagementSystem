using Domain.Entities;
using Microsoft.EntityFrameworkCore;

public interface IApplicationDbContext
{
    // Example:
    DbSet<Candidate> Candidates { get; set; }
    DbSet<Job> Jobs { get; set; }
     DbSet<Requisition> Requisitions { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<JobApplication> JobApplications { get; }

    DbSet<Interview> Interviews { get; }









    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}