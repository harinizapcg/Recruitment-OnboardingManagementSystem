public class Interview
{
    public int Id { get; set; }

    public int JobApplicationId { get; set; }

    public DateTime InterviewDate { get; set; }

    public string Interviewer { get; set; }

    public string Status { get; set; } = "Scheduled"; // Scheduled, Completed
}