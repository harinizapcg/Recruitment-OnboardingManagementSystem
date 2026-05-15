namespace ROMSS.UI.Models.DTO
{
    public class AddUserRequestDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}