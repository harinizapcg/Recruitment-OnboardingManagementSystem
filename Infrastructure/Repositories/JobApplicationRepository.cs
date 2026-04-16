using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class JobApplicationRepository : IJobApplicationRepository
{
    private readonly ApplicationDbContext _context;

    public JobApplicationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // ✅ Create
    public async Task<JobApplication> CreateAsync(JobApplication application)
    {
        _context.JobApplications.Add(application);
        await _context.SaveChangesAsync();
        return application;
    }

    // ✅ Get by Id
    public async Task<JobApplication?> GetByIdAsync(int id)
    {
        return await _context.JobApplications.FindAsync(id);
    }

    // ✅ Get All
    public async Task<List<JobApplication>> GetAllAsync()
    {
        return await _context.JobApplications.ToListAsync();
    }

    // ✅ Update
    public async Task UpdateAsync(JobApplication application)
    {
        _context.JobApplications.Update(application);
        await _context.SaveChangesAsync();
    }

    // ✅ Delete
    public async Task DeleteAsync(int id)
    {
        var entity = await _context.JobApplications.FindAsync(id);

        if (entity != null)
        {
            _context.JobApplications.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    // ✅ Update Status (VERY useful for next step)
    public async Task UpdateStatusAsync(int id, string status)
    {
        var entity = await _context.JobApplications.FindAsync(id);

        if (entity == null)
            throw new Exception("Application not found");

        entity.Status = status;

        await _context.SaveChangesAsync();
    }
}