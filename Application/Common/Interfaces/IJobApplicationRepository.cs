using Domain.Entities;

public interface IJobApplicationRepository
{
    Task<JobApplication> CreateAsync(JobApplication application);

    Task<JobApplication?> GetByIdAsync(int id);

    Task<List<JobApplication>> GetAllAsync();

    Task UpdateAsync(JobApplication application);

    Task DeleteAsync(int id);
}