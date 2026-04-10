using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
            => await _context.Users
                .Include(u => u.Role)
                .AsNoTracking()
                .ToListAsync();

        public async Task<User?> GetByIdAsync(int id)
            => await _context.Users
                .Include(u => u.Role)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

        public async Task<User?> GetByEmailAsync(string email)
            => await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);

        public async Task<User> CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync(CancellationToken.None);
            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync(CancellationToken.None);
            return user;
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user is not null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync(CancellationToken.None);
            }
        }

        public async Task<bool> ExistsAsync(int id)
            => await _context.Users.AnyAsync(u => u.Id == id);

        public async Task<bool> EmailExistsAsync(string email)
            => await _context.Users.AnyAsync(u => u.Email == email);
    }
}