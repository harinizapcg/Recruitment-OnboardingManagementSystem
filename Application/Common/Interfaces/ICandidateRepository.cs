using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICandidateRepository
    {
        Task<IEnumerable<Candidate>> GetAllAsync();
        Task<Candidate?> GetByIdAsync(int id);
        Task<Candidate> CreateAsync(Candidate candidate);
        Task<Candidate> UpdateAsync(Candidate candidate);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> EmailExistsAsync(string email);
    }
}