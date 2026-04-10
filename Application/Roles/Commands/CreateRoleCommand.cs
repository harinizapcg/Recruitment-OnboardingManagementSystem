using MediatR;

namespace Application.Roles.Commands
{
    public class CreateRoleCommand : IRequest<int>
    {
        public string RoleName { get; set; } = string.Empty;
        public string RoleDescription { get; set; } = string.Empty;
    }
}