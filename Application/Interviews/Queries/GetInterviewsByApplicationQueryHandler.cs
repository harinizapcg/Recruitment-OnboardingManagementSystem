using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Interviews.Queries;

public class InterviewResult
{
    public int Id { get; set; }
    public int JobApplicationId { get; set; }
    public DateTime InterviewDate { get; set; }
    public string Interviewer { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}

public class GetInterviewsByApplicationQueryHandler
    : IRequestHandler<GetInterviewsByApplicationQuery, List<InterviewResult>>
{
    private readonly IApplicationDbContext _context;

    public GetInterviewsByApplicationQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<InterviewResult>> Handle(
        GetInterviewsByApplicationQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Interviews
            .Where(x => x.JobApplicationId == request.JobApplicationId)
            .Select(x => new InterviewResult
            {
                Id = x.Id,
                JobApplicationId = x.JobApplicationId,
                InterviewDate = x.InterviewDate,
                Interviewer = x.Interviewer,
                Status = x.Status
            })
            .ToListAsync(cancellationToken);
    }
}