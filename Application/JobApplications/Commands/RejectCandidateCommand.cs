using MediatR;

namespace Application.JobApplications.Commands;

public class RejectCandidateCommand : IRequest
{
    public int ApplicationId { get; set; }
    public string? Comments { get; set; }
}