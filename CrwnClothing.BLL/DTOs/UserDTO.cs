
namespace CrwnClothing.BLL.DTOs
{
    public class UserDTO
    {
        public int? Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public bool? Verified { get; set; }
    }
}
