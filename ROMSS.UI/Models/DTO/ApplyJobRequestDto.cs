using Microsoft.AspNetCore.Http;

namespace ROMSS.UI.Models.DTO
{
    public class ApplyJobRequestDto
    {
        public int JobId { get; set; }
        public int CandidateId { get; set; }
        public IFormFile Resume { get; set; } = null!;
        public IFormFile? CoverLetter { get; set; }
    }
}