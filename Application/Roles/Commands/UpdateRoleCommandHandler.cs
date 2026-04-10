using Application.Roles.Commands;
using Domain.Interfaces;
using MediatR;

namespace Application.Roles.Handlers
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, bool>
    {
        private readonly IRoleRepository _roleRepository;

        public UpdateRoleCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<bool> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetByIdAsync(request.RoleId);
            if (role is null)
                throw new Exception($"Role with ID {request.RoleId} not found.");

            if (role.RoleName != request.RoleName && await _roleRepository.NameExistsAsync(request.RoleName))
                throw new Exception($"Role name '{request.RoleName}' is already taken.");

            role.RoleName = request.RoleName;
            role.RoleDescription = request.RoleDescription;

            await _roleRepository.UpdateAsync(role);
            return true;
        }
    }
}