using MediatR;

namespace Application.JobApplications.Commands;

public class ShortlistCandidateCommand : IRequest
{
    public int ApplicationId { get; set; }
    public string? Comments { get; set; }
}