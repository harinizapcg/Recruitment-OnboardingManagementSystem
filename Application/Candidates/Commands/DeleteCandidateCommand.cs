using MediatR;

namespace Application.Candidates.Commands
{
    public class DeleteCandidateCommand : IRequest
    {
        public int Id { get; set; }
    }
}