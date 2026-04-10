using MediatR;

namespace Application.Roles.Commands
{
    public class DeleteRoleCommand : IRequest<bool>
    {
        public int RoleId { get; set; }
    }
}