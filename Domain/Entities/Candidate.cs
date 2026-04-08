namespace Domain.Entities
{
    public class Candidate : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Skills { get; set; } = string.Empty;
        public int Experience { get; set; }
        public string ResumePath { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;

        public ICollection<JobApplication> Applications { get; set; } = new List<JobApplication>();
    }
}