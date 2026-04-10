using MediatR;

namespace Application.Candidates.Commands
{
    public class UpdateCandidateCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Skills { get; set; } = string.Empty;
        public int Experience { get; set; }
        public string ResumePath { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
    }
}