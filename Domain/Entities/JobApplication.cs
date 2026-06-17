public class JobApplication
{
    public int Id { get; set; }
    public int JobId { get; set; }
    public int CandidateId { get; set; }
    public string Status { get; set; } = "Pending";
    public DateTime AppliedAt { get; set; } = DateTime.UtcNow;
    public string? ScreeningComments { get; set; }
    public string? ResumePath { get; set; }
    public string? CoverLetterPath { get; set; }
}