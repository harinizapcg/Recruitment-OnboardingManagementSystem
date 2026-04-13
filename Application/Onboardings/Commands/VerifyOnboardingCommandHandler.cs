using MediatR;

public class VerifyOnboardingCommandHandler
    : IRequestHandler<VerifyOnboardingCommand, bool>
{
    private readonly IOnboardingRepository _repository;

    public VerifyOnboardingCommandHandler(IOnboardingRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(
        VerifyOnboardingCommand request,
        CancellationToken cancellationToken)
    {
        return await _repository.VerifyAsync(request.ApplicationId);
    }
}