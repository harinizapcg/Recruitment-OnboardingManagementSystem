using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.JobApplications.Queries;

public class GetApplicationsByJobQueryHandler
    : IRequestHandler<GetApplicationsByJobQuery, List<JobApplication>>
{
    private readonly IApplicationDbContext _context;

    public GetApplicationsByJobQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<JobApplication>> Handle(
        GetApplicationsByJobQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.JobApplications
            .Where(x => x.JobId == request.JobId)
            .ToListAsync(cancellationToken);
    }
}