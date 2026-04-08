using MediatR;

namespace Application.Interviews.Queries;

public class GetInterviewsByApplicationQuery : IRequest<List<Interview>>
{
    public int JobApplicationId { get; set; }
}