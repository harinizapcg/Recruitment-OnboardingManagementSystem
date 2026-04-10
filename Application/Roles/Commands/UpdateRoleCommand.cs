using MediatR;

namespace Application.Roles.Commands
{
    public class UpdateRoleCommand : IRequest<bool>
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public string RoleDescription { get; set; } = string.Empty;
    }
}