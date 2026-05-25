using Application.Common.Interfaces;
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
            .Join(_context.Candidates,
                app => app.CandidateId,
                c => c.Id,
                (app, c) => new JobApplicationResult
                {
                    Id = app.Id,
                    JobId = app.JobId,
                    CandidateId = app.CandidateId,
                    CandidateName = c.Name,
                    Status = app.Status,
                    AppliedAt = app.AppliedAt,
                    ScreeningComments = app.ScreeningComments,
                    ResumePath = app.ResumePath ?? string.Empty,
                    CoverLetterPath = app.CoverLetterPath ?? string.Empty
                })
            .ToListAsync(cancellationToken);
    }
}