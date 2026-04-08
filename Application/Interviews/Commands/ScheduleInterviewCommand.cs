using MediatR;

namespace Application.Interviews.Commands;

public class ScheduleInterviewCommand : IRequest<int>
{
    public int JobApplicationId { get; set; }
    public DateTime InterviewDate { get; set; }
    public string Interviewer { get; set; }
}