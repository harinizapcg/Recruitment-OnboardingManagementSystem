using MediatR;

namespace Application.Onboardings.Handlers;

public class GetOnboardingByApplicationIdHandler
    : IRequestHandler<GetOnboardingByApplicationIdQuery, Onboarding>
{
    private readonly IOnboardingRepository _repository;

    public GetOnboardingByApplicationIdHandler(IOnboardingRepository repository)
    {
        _repository = repository;
    }

    public async Task<Onboarding> Handle(
        GetOnboardingByApplicationIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.GetByApplicationIdAsync(request.ApplicationId);
    }
}