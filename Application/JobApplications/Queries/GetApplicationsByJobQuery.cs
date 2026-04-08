using MediatR;

namespace Application.JobApplications.Queries;

public class GetApplicationsByJobQuery : IRequest<List<JobApplication>>
{
    public int JobId { get; set; }
}