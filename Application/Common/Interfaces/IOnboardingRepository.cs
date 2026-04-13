public interface IOnboardingRepository
{
    Task<int> CreateAsync(Onboarding onboarding);
    Task<bool> VerifyAsync(int applicationId);
    Task<Onboarding> GetByApplicationIdAsync(int applicationId);
}