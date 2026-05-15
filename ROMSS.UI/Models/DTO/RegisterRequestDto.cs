public class RegisterRequestDto
{
    public string Name { get; set; }      // ✅ required
    public string Email { get; set; }
    public string Password { get; set; }
    public int RoleId { get; set; }       // ✅ must be int
}