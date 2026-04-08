using MediatR;

namespace Application.JobApplications.Commands;

public class ApplyToJobCommand : IRequest<int>
{
    public int JobId { get; set; }
    public int CandidateId { get; set; }
}