namespace Domain.Entities
{
    public class Requisition : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string RequiredSkills { get; set; } = string.Empty;
        public int ExperienceRequired { get; set; }
        public string Priority { get; set; } = "Medium";
        public string Status { get; set; } = "Open";
        public int? CreatedBy { get; set; }

        public User? CreatedByUser { get; set; }
        public ICollection<Job> Jobs { get; set; } = new List<Job>();
    }
}

