using MediatR;

public class SubmitFeedbackCommand : IRequest<int>
{
    public int ApplicationId { get; set; }
    public int InterviewId { get; set; }
    public int InterviewerId { get; set; }
    public int Rating { get; set; }
    public string Comments { get; set; }
}