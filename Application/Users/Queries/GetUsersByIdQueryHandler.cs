using Application.Users.DTOs;
using Application.Users.Queries;
using Domain.Interfaces;
using MediatR;

namespace Application.Users.Handlers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user is null)
                throw new Exception($"User with ID {request.UserId} not found.");

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                RoleId = user.RoleId,
                RoleName = user.Role?.RoleName ?? string.Empty,
                IsActive = user.IsActive
            };
        }
    }
}