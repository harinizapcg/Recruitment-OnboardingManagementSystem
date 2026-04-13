using MediatR;

public class GetFeedbackByApplicationIdHandler
    : IRequestHandler<GetFeedbackByApplicationIdQuery, List<InterviewFeedback>>
{
    private readonly IInterviewFeedbackRepository _repository;

    public GetFeedbackByApplicationIdHandler(IInterviewFeedbackRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<InterviewFeedback>> Handle(
        GetFeedbackByApplicationIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.GetByApplicationIdAsync(request.ApplicationId);
    }
}