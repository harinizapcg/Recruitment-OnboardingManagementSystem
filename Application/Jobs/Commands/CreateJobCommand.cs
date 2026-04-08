using MediatR;

namespace Application.Jobs.Commands
{
    public class CreateJobCommand : IRequest<int>
    {
        public int RequisitionId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string RequiredSkills { get; set; } = string.Empty;
        public int ExperienceRequired { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Status { get; set; } = "Active";
    }
}