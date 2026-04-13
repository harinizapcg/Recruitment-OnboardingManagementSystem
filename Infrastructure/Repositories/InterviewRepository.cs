using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class InterviewRepository : IInterviewRepository
    {
        private readonly ApplicationDbContext _context;

        public InterviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Interview?> GetByIdAsync(int id)
            => await _context.Interviews
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id);

        public async Task<IEnumerable<Interview>> GetByApplicationIdAsync(int applicationId)
            => await _context.Interviews
                .AsNoTracking()
                .Where(i => i.JobApplicationId == applicationId)
                .ToListAsync();

        public async Task<Interview> CreateAsync(Interview interview)
        {
            _context.Interviews.Add(interview);
            await _context.SaveChangesAsync(CancellationToken.None);
            return interview;
        }

        public async Task<Interview> UpdateAsync(Interview interview)
        {
            _context.Interviews.Update(interview);
            await _context.SaveChangesAsync(CancellationToken.None);
            return interview;
        }

        public async Task<bool> ExistsAsync(int id)
            => await _context.Interviews.AnyAsync(i => i.Id == id);
    }
}