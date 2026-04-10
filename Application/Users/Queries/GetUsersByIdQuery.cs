using Application.Users.DTOs;
using MediatR;

namespace Application.Users.Queries
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public int UserId { get; set; }
    }
}