using MediatR;

namespace Application.Candidates.Commands
{
    public class UpdateCandidateCommand : IRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Skills { get; set; }
        public int? Experience { get; set; }
        public string? ResumePath { get; set; }
        public string? Source { get; set; }
    }
}