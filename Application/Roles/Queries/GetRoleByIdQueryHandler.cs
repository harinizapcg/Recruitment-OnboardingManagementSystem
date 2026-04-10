using Application.Roles.Queries;
using Application.Roles.DTOs;
using Domain.Interfaces;
using MediatR;

namespace Application.Roles.Handlers
{
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, RoleDto>
    {
        private readonly IRoleRepository _roleRepository;

        public GetRoleByIdQueryHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<RoleDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetByIdAsync(request.RoleId);
            if (role is null)
                throw new Exception($"Role with ID {request.RoleId} not found.");

            return new RoleDto
            {
                Id = role.Id,
                RoleName = role.RoleName,
                RoleDescription = role.RoleDescription
            };
        }
    }
}