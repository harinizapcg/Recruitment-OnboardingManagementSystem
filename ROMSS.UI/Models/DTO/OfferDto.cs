namespace ROMSS.UI.Models.DTO
{
    public class OfferDto
    {
        public int OfferId { get; set; }
        public int Id { get; set; }                          // ✅ add
        public int ApplicationId { get; set; }
        public decimal Salary { get; set; }
        public DateTime JoiningDate { get; set; }
        public string Status { get; set; } = "Pending";
        public string OfferLetter { get; set; } = string.Empty;  // ✅ add
        public DateTime CreatedAt { get; set; }
        public DateTime CreatedDate { get; set; }            // ✅ add
    }

    public class GenerateOfferRequestDto
    {
        public int ApplicationId { get; set; }
        public decimal Salary { get; set; }
        public DateTime JoiningDate { get; set; }
    }
}