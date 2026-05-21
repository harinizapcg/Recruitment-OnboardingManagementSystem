namespace ROMSS.UI.Models.DTO
{
    public class JobApplicationDto
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int CandidateId { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime AppliedAt { get; set; }
        public string? ScreeningComments { get; set; }
        public string ResumePath { get; set; } = string.Empty;
        public string CoverLetterPath { get; set; } = string.Empty;
    }

    public class ApplyJobRequestDto
    {
        public int JobId { get; set; }
        public int CandidateId { get; set; }
        public string ResumePath { get; set; } = string.Empty;
        public string CoverLetterPath { get; set; } = string.Empty;
    }
}