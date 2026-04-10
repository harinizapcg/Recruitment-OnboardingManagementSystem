using Application.Roles.DTOs;
using MediatR;

namespace Application.Roles.Queries
{
    public class GetAllRolesQuery : IRequest<List<RoleDto>>
    {
    }
}