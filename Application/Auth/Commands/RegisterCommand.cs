using MediatR;

namespace Application.Auth.Commands
{
    public class RegisterCommand : IRequest<int>
    {
        public string Name { get; set; } = string.Empty;   // ✅ FIXED
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int RoleId { get; set; }
    }
}