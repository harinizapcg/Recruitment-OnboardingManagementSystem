namespace ROMSS.UI.Models.DTO
{
    public class OnboardingDto
    {
        public int OnboardingId { get; set; }
        public int ApplicationId { get; set; }
        public string DocumentPath { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; }
    }

    public class UploadDocumentsRequestDto
    {
        public int ApplicationId { get; set; }
        public string DocumentPath { get; set; } = string.Empty;
    }
}