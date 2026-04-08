using MediatR;

namespace Application.Requisitions.Commands
{
    public class CreateRequisitionCommand : IRequest<int>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string RequiredSkills { get; set; } = string.Empty;
        public int ExperienceRequired { get; set; }
        public string Priority { get; set; } = "Medium";
        public string Status { get; set; } = "Open";
        public int? CreatedBy { get; set; }
    }
}