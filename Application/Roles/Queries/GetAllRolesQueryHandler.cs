using Application.Roles.Queries;
using Application.Roles.DTOs;
using Domain.Interfaces;
using MediatR;

namespace Application.Roles.Handlers
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, List<RoleDto>>
    {
        private readonly IRoleRepository _roleRepository;

        public GetAllRolesQueryHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<List<RoleDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roleRepository.GetAllAsync();

            return roles.Select(r => new RoleDto
            {
                Id = r.Id,
                RoleName = r.RoleName,
                RoleDescription = r.RoleDescription
            }).ToList();
        }
    }
}