using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class InterviewFeedbackRepository : IInterviewFeedbackRepository
{
    private readonly ApplicationDbContext _context;

    public InterviewFeedbackRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(InterviewFeedback feedback)
    {
        _context.InterviewFeedbacks.Add(feedback);
        await _context.SaveChangesAsync();
        return feedback.FeedbackId;
    }

    public async Task<List<InterviewFeedback>> GetByApplicationIdAsync(int applicationId)
    {
        return await _context.InterviewFeedbacks
            .Where(f => f.ApplicationId == applicationId)
            .ToListAsync();
    }
}