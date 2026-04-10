using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
            => await _context.Roles
                .AsNoTracking()
                .ToListAsync();

        public async Task<Role?> GetByIdAsync(int id)
            => await _context.Roles
                .Include(r => r.Users)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);

        public async Task<Role> CreateAsync(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync(CancellationToken.None);
            return role;
        }

        public async Task<Role> UpdateAsync(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync(CancellationToken.None);
            return role;
        }

        public async Task DeleteAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role is not null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync(CancellationToken.None);
            }
        }

        public async Task<bool> ExistsAsync(int id)
            => await _context.Roles.AnyAsync(r => r.Id == id);

        public async Task<bool> NameExistsAsync(string roleName)
            => await _context.Roles.AnyAsync(r => r.RoleName == roleName);
    }
}