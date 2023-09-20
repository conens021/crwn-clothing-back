using Newtonsoft.Json;

namespace CrwnClothing.BLL.External.Contracts
{
    public partial class GoogleUserInfo : BaseUserInfo
    {
        [JsonProperty("verified_email")]
        public bool VerifiedEmail { get; set; }

        [JsonProperty("given_name")]
        public string GivenName { get; set; } = String.Empty;

        [JsonProperty("family_name")]
        public string FamilyName { get; set; } = String.Empty;

        [JsonProperty("picture")]
        public Uri GooglePicture { get; set; } = null!;

        [JsonProperty("locale")]
        public string Locale { get; set; } = String.Empty;
    }
}
