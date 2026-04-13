using MediatR;

public class GetOnboardingByApplicationIdQuery : IRequest<Onboarding>
{
    public int ApplicationId { get; set; }
}