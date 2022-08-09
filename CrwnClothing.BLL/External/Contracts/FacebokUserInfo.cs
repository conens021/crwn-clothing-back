using Newtonsoft.Json;

namespace CrwnClothing.BLL.External.Contracts
{
    public class FacebokUserInfo
    {

        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("email")]
        public string Email { get; set; } = string.Empty;

        [JsonProperty("picture")]
        public FacebookPicture Picture { get; set; } = null!;


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
