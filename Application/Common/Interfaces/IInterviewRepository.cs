using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IInterviewRepository
    {
        Task<Interview?> GetByIdAsync(int id);
        Task<IEnumerable<Interview>> GetByApplicationIdAsync(int applicationId);
        Task<Interview> CreateAsync(Interview interview);
        Task<Interview> UpdateAsync(Interview interview);
        Task<bool> ExistsAsync(int id);
    }
}