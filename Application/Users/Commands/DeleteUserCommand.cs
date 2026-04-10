using MediatR;

namespace Application.Users.Commands
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public int UserId { get; set; }
    }
}