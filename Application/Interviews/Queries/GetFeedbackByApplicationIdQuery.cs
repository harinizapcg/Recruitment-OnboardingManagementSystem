using MediatR;

public class GetFeedbackByApplicationIdQuery : IRequest<List<InterviewFeedback>>
{
    public int ApplicationId { get; set; }
}