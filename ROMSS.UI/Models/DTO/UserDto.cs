namespace ROMSS.UI.Models.DTO
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public bool IsActive { get; set; }
    }
}