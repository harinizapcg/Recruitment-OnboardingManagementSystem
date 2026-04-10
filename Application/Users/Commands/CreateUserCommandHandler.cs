using Application.Users.Commands;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Users.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public CreateUserCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var roleExists = await _roleRepository.ExistsAsync(request.RoleId);
            if (!roleExists)
                throw new Exception($"Role with ID {request.RoleId} does not exist.");

            var emailExists = await _userRepository.EmailExistsAsync(request.Email);
            if (emailExists)
                throw new Exception($"Email '{request.Email}' is already registered.");

            var user = new User
            {
                Name = request.FullName,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                RoleId = request.RoleId,
                IsActive = true
            };

            await _userRepository.CreateAsync(user);
            return user.Id;
        }
    }
}