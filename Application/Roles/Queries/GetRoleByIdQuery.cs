using Application.Roles.DTOs;
using MediatR;

namespace Application.Roles.Queries
{
    public class GetRoleByIdQuery : IRequest<RoleDto>
    {
        public int RoleId { get; set; }
    }
}