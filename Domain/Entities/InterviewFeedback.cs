using System.ComponentModel.DataAnnotations;

public class InterviewFeedback
{
    [Key] // ✅ ADD THIS
    public int FeedbackId { get; set; }

    public int ApplicationId { get; set; }
    public int InterviewId { get; set; }
    public int InterviewerId { get; set; }

    public int Rating { get; set; }
    public string Comments { get; set; }

    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
}