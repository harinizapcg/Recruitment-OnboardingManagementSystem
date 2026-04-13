public interface IInterviewFeedbackRepository
{
    Task<int> AddAsync(InterviewFeedback feedback);
    Task<List<InterviewFeedback>> GetByApplicationIdAsync(int applicationId);
}