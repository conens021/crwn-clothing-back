using Newtonsoft.Json;

namespace CrwnClothing.BLL.External.Contracts
{
    public class BaseUserInfo
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("name")]
        public string Name { get; set; } = String.Empty;

        [JsonProperty("email")]
        public string Email { get; set; } = string.Empty;
    }
}
