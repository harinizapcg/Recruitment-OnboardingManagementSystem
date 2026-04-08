using MediatR;

namespace Application.Jobs.Commands
{
    public class DeleteJobCommand : IRequest
    {
        public int Id { get; set; }
    }
}