using MediatR;

namespace Application.Jobs.Commands
{
    public class UpdateJobCommand : IRequest
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? RequiredSkills { get; set; }
        public int? ExperienceRequired { get; set; }
        public string? Location { get; set; }
        public string? Status { get; set; }
    }
}