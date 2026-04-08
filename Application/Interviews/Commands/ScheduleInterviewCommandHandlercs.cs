using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Interviews.Commands;

public class ScheduleInterviewCommandHandler
    : IRequestHandler<ScheduleInterviewCommand, int>
{
    private readonly IApplicationDbContext _context;

    public ScheduleInterviewCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(ScheduleInterviewCommand request, CancellationToken cancellationToken)
    {
        // ✅ Check application exists
        var exists = await _context.JobApplications
            .AnyAsync(x => x.Id == request.JobApplicationId);

        if (!exists)
            throw new Exception("Job Application not found");

        var interview = new Interview
        {
            JobApplicationId = request.JobApplicationId,
            InterviewDate = request.InterviewDate,
            Interviewer = request.Interviewer,
            Status = "Scheduled"
        };

        _context.Interviews.Add(interview);
        await _context.SaveChangesAsync(cancellationToken);

        return interview.Id;
    }
}