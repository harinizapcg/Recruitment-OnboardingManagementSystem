using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.JobApplications.Queries;

public class GetApplicationsByJobQueryHandler
    : IRequestHandler<GetApplicationsByJobQuery, List<JobApplicationResult>>
{
    private readonly IApplicationDbContext _context;

    public GetApplicationsByJobQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<JobApplicationResult>> Handle(
        GetApplicationsByJobQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.JobApplications
            .Where(x => x.JobId == request.JobId)
            .Select(x => new JobApplicationResult
            {
                Id = x.Id,
                JobId = x.JobId,
                CandidateId = x.CandidateId,
                Status = x.Status,
                AppliedAt = x.AppliedAt,
                ScreeningComments = x.ScreeningComments,
                ResumePath = x.ResumePath ?? string.Empty,
                CoverLetterPath = x.CoverLetterPath ?? string.Empty
            })
            .ToListAsync(cancellationToken);
    }
}