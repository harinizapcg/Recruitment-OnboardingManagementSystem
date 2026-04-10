using Application.Roles.Commands;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Roles.Handlers
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, int>
    {
        private readonly IRoleRepository _roleRepository;

        public CreateRoleCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<int> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var nameExists = await _roleRepository.NameExistsAsync(request.RoleName);
            if (nameExists)
                throw new Exception($"Role '{request.RoleName}' already exists.");

            var role= new Domain.Entities.Role
            {
                RoleName = request.RoleName,
                RoleDescription = request.RoleDescription
            };

            await _roleRepository.CreateAsync(role);
            return role.Id;
        }
    }
}