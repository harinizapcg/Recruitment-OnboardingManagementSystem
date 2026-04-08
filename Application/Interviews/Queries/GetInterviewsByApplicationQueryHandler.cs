using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Interviews.Queries;

public class GetInterviewsByApplicationQueryHandler
    : IRequestHandler<GetInterviewsByApplicationQuery, List<Interview>>
{
    private readonly IApplicationDbContext _context;

    public GetInterviewsByApplicationQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Interview>> Handle(
        GetInterviewsByApplicationQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Interviews
            .Where(x => x.JobApplicationId == request.JobApplicationId)
            .ToListAsync(cancellationToken);
    }
}