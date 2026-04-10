using MediatR;

namespace Application.Users.Commands
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
    }
}