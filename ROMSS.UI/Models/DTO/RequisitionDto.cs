namespace ROMSS.UI.Models.DTO
{
    public class RequisitionDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string RequiredSkills { get; set; } = string.Empty;
        public int ExperienceRequired { get; set; }
        public string Priority { get; set; } = "Medium";
        public string Status { get; set; } = "Open";
    }

    public class AddRequisitionRequestDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string RequiredSkills { get; set; } = string.Empty;
        public int ExperienceRequired { get; set; }
        public string Priority { get; set; } = "Medium";
        public string Status { get; set; } = "Open";
    }

    public class UpdateRequisitionRequestDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string RequiredSkills { get; set; } = string.Empty;
        public int ExperienceRequired { get; set; }
        public string Priority { get; set; } = "Medium";
        public string Status { get; set; } = "Open";
    }
}