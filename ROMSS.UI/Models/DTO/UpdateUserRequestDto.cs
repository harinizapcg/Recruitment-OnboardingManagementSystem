namespace ROMSS.UI.Models.DTO
{
    public class UpdateUserRequestDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
    }
}