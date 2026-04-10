using Application.Users.Commands;
using Domain.Interfaces;
using MediatR;

namespace Application.Users.Handlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var exists = await _userRepository.ExistsAsync(request.UserId);
            if (!exists)
                throw new Exception($"User with ID {request.UserId} not found.");

            await _userRepository.DeleteAsync(request.UserId);
            return true;
        }
    }
}