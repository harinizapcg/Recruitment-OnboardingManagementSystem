public class Onboarding
{
    public int OnboardingId { get; set; }

    public int ApplicationId { get; set; }

    public string DocumentPath { get; set; }

    public string Status { get; set; } = "Pending";
    // Pending, Verified, Rejected

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}