namespace ROMSS.UI.Models.DTO
{
    public class InterviewDto
    {
        public int Id { get; set; }
        public int JobApplicationId { get; set; }
        public DateTime InterviewDate { get; set; }
        public string Interviewer { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }

    public class ScheduleInterviewRequestDto
    {
        public int JobApplicationId { get; set; }
        public DateTime InterviewDate { get; set; }
        public string Interviewer { get; set; } = string.Empty;
    }

    public class InterviewFeedbackDto
    {
        public int FeedbackId { get; set; }
        public int ApplicationId { get; set; }
        public int InterviewId { get; set; }
        public int InterviewerId { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty; // Selected / Rejected
        public DateTime SubmittedAt { get; set; }
    }

    public class SubmitFeedbackRequestDto
    {
        public int ApplicationId { get; set; }
        public int InterviewId { get; set; }
        public int InterviewerId { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty; // Selected / Rejected
    }
}