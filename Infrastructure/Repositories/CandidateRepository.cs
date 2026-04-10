using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly ApplicationDbContext _context;

        public CandidateRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Candidate>> GetAllAsync()
            => await _context.Candidates
                .AsNoTracking()
                .ToListAsync();

        public async Task<Candidate?> GetByIdAsync(int id)
            => await _context.Candidates
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

        public async Task<Candidate> CreateAsync(Candidate candidate)
        {
            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync(CancellationToken.None);
            return candidate;
        }

        public async Task<Candidate> UpdateAsync(Candidate candidate)
        {
            _context.Candidates.Update(candidate);
            await _context.SaveChangesAsync(CancellationToken.None);
            return candidate;
        }

        public async Task DeleteAsync(int id)
        {
            var candidate = await _context.Candidates.FindAsync(id);
            if (candidate is not null)
            {
                _context.Candidates.Remove(candidate);
                await _context.SaveChangesAsync(CancellationToken.None);
            }
        }

        public async Task<bool> ExistsAsync(int id)
            => await _context.Candidates.AnyAsync(c => c.Id == id);

        public async Task<bool> EmailExistsAsync(string email)
            => await _context.Candidates.AnyAsync(c => c.Email == email);
    }
}