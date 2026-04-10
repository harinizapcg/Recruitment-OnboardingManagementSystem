using Application.Users.Commands;
using Domain.Interfaces;
using MediatR;

namespace Application.Users.Handlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user is null)
                throw new Exception($"User with ID {request.UserId} not found.");

            var roleExists = await _roleRepository.ExistsAsync(request.RoleId);
            if (!roleExists)
                throw new Exception($"Role with ID {request.RoleId} does not exist.");

            var emailOwner = await _userRepository.GetByEmailAsync(request.Email);
            if (emailOwner is not null && emailOwner.Id != request.UserId)
                throw new Exception($"Email '{request.Email}' is already in use.");

            user.Name = request.FullName;
            user.Email = request.Email;
            user.RoleId = request.RoleId;
            user.IsActive = request.IsActive;

            await _userRepository.UpdateAsync(user);
            return true;
        }
    }
}