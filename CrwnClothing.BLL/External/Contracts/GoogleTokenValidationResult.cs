using Newtonsoft.Json;

namespace CrwnClothing.BLL.External.Contracts
{
    public partial class GoogleTokenValidationResult
    {
        [JsonProperty("issued_to")]
        public string IssuedTo { get; set; } = String.Empty;

        [JsonProperty("audience")]
        public string Audience { get; set; } = String.Empty;

        [JsonProperty("user_id")]
        public string UserId { get; set; } = String.Empty;

        [JsonProperty("scope")]
        public string Scope { get; set; } = String.Empty;

        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; } = String.Empty;

        [JsonProperty("verified_email")]
        public bool VerifiedEmail { get; set; }

        [JsonProperty("access_type")]
        public string AccessType { get; set; } = String.Empty;
    }

}
