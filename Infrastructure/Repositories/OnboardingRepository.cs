using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class OnboardingRepository : IOnboardingRepository
{
    private readonly ApplicationDbContext _context;

    public OnboardingRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateAsync(Onboarding onboarding)
    {
        _context.Onboardings.Add(onboarding);
        await _context.SaveChangesAsync();
        return onboarding.OnboardingId;
    }

    public async Task<bool> VerifyAsync(int applicationId)
    {
        var onboarding = await _context.Onboardings
            .FirstOrDefaultAsync(o => o.ApplicationId == applicationId);

        if (onboarding == null)
            return false;

        onboarding.Status = "Verified";

        // 🔥 FINAL STEP
        var application = await _context.JobApplications.FindAsync(applicationId);
        if (application != null)
            application.Status = "Joined";

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Onboarding> GetByApplicationIdAsync(int applicationId)
    {
        return await _context.Onboardings
            .FirstOrDefaultAsync(o => o.ApplicationId == applicationId);
    }
}