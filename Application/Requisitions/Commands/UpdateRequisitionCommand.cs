using MediatR;

namespace Application.Requisitions.Commands
{
    public class UpdateRequisitionCommand : IRequest
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? RequiredSkills { get; set; }
        public int? ExperienceRequired { get; set; }
        public string? Priority { get; set; }
        public string? Status { get; set; }
    }
}