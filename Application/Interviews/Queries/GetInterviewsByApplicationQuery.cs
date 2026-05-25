using MediatR;
using Application.Interviews.Queries;

namespace Application.Interviews.Queries;

public class GetInterviewsByApplicationQuery : IRequest<List<InterviewResult>>
{
    public int JobApplicationId { get; set; }
}