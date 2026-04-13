using MediatR;

public class SubmitFeedbackCommandHandler
    : IRequestHandler<SubmitFeedbackCommand, int>
{
    private readonly IInterviewFeedbackRepository _repository;

    public SubmitFeedbackCommandHandler(IInterviewFeedbackRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(SubmitFeedbackCommand request, CancellationToken cancellationToken)
    {
        if (request.Rating < 1 || request.Rating > 5)
            throw new Exception("Rating must be between 1 and 5");

        var feedback = new InterviewFeedback
        {
            ApplicationId = request.ApplicationId,
            InterviewId = request.InterviewId,
            InterviewerId = request.InterviewerId,
            Rating = request.Rating,
            Comments = request.Comments
        };

        return await _repository.AddAsync(feedback);
    }
}