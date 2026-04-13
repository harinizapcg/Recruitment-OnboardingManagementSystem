public class Offer
{
    public int OfferId { get; set; }

    public int ApplicationId { get; set; }

    public decimal Salary { get; set; }
    public DateTime JoiningDate { get; set; }

    public string Status { get; set; } = "Pending";
    // Pending, Accepted, Rejected

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}