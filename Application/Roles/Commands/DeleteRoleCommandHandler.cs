using Application.Roles.Commands;
using Domain.Interfaces;
using MediatR;

namespace Application.Roles.Handlers
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, bool>
    {
        private readonly IRoleRepository _roleRepository;

        public DeleteRoleCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetByIdAsync(request.RoleId);
            if (role is null)
                throw new Exception($"Role with ID {request.RoleId} not found.");

            if (role.Users.Any())
                throw new Exception("Cannot delete a role that has assigned users.");

            await _roleRepository.DeleteAsync(request.RoleId);
            return true;
        }
    }
}