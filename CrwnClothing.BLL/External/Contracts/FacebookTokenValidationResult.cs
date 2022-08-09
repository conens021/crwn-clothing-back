using Newtonsoft.Json;

namespace CrwnClothing.BLL.External.Contracts
{
    public class FacebookTokenValidationResult
    {
        [JsonProperty("data")]
        public Data Data { get; set; } = null!;
    }

    public class Data
    {
        [JsonProperty("app_id")]
        public string AppId { get; set; } = string.Empty;

        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;

        [JsonProperty("application")]
        public string Application { get; set; } = string.Empty;

        [JsonProperty("data_access_expires_at")]
        public long DataAccessExpiresAt { get; set; }

        [JsonProperty("expires_at")]
        public long ExpiresAt { get; set; }

        [JsonProperty("is_valid")]
        public bool IsValid { get; set; }

        [JsonProperty("scopes")]
        public string[] Scopes { get; set; } = null!;

        [JsonProperty("user_id")]
        public string UserId { get; set; } = string.Empty;
    }
}
