using System.ComponentModel.DataAnnotations;

namespace ROMSS.UI.Models.DTO
{
    public class UpdateCandidateRequestDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be at least 3 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone must be exactly 10 digits")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Skills are required")]
        [StringLength(500, MinimumLength = 2, ErrorMessage = "Skills must be at least 2 characters")]
        public string Skills { get; set; } = string.Empty;

        [Required(ErrorMessage = "Experience is required")]
        [Range(0, 50, ErrorMessage = "Experience must be between 0 and 50 years")]
        public int Experience { get; set; }

        public string ResumePath { get; set; } = string.Empty;

        [Required(ErrorMessage = "Source is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Source must be at least 2 characters")]
        public string Source { get; set; } = string.Empty;
    }
}