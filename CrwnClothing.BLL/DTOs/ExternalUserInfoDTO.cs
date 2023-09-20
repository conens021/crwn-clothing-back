namespace CrwnClothing.BLL.DTOs
{
    public class ExternalUserInfoDTO
    {
        public string ExternalId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public Uri Picture { get; set; } = null!;
    }
}
