using MediatR;

public class UploadDocumentsCommandHandler
    : IRequestHandler<UploadDocumentsCommand, int>
{
    private readonly IOnboardingRepository _repository;

    public UploadDocumentsCommandHandler(IOnboardingRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(
        UploadDocumentsCommand request,
        CancellationToken cancellationToken)
    {
        var onboarding = new Onboarding
        {
            ApplicationId = request.ApplicationId,
            DocumentPath = request.DocumentPath
        };

        return await _repository.CreateAsync(onboarding);
    }
}