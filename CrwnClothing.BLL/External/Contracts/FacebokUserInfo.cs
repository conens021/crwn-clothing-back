using Newtonsoft.Json;

namespace CrwnClothing.BLL.External.Contracts
{
    public class FacebokUserInfo : BaseUserInfo
    {
        [JsonProperty("picture")]
        public FacebookPicture Picture { get; set; } = null!;

        [JsonProperty("first_name")]
        public string FirstName { get; set; } = String.Empty;

        [JsonProperty("last_name")]
        public string LastName { get; set; } = String.Empty;

        public class FacebookPicture
        {
            [JsonProperty("data")]
            public Data Data { get; set; } = null!;
        }

        public class Data
        {
            [JsonProperty("height")]
            public long Height { get; set; }

            [JsonProperty("is_silhouette")]
            public bool IsSilhouette { get; set; }

            [JsonProperty("url")]
            public Uri Url { get; set; }

            [JsonProperty("width")]
            public long Width { get; set; }
        }
    }
}
