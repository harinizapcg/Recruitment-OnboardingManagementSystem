using MediatR;

public class VerifyOnboardingCommand : IRequest<bool>
{
    public int ApplicationId { get; set; }
}