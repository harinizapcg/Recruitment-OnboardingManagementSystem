using Application.Users.DTOs;
using Application.Users.Queries;
using Domain.Interfaces;
using MediatR;

namespace Application.Users.Handlers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();

            return users.Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                PasswordHash = u.PasswordHash,
                RoleId = u.RoleId,
                RoleName = u.Role?.RoleName ?? string.Empty,
                IsActive = u.IsActive
            }).ToList();
        }
    }
}