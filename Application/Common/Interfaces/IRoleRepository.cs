using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllAsync();
        Task<Role?> GetByIdAsync(int id);
        Task<Role> CreateAsync(Role role);
        Task<Role> UpdateAsync(Role role);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> NameExistsAsync(string roleName);
    }
}