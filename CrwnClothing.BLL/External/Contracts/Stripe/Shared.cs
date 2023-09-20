using Newtonsoft.Json;

namespace CrwnClothing.BLL.External.Contracts.Stripe
{
    public partial class StripePaymentMethodDetails
    {
        [JsonProperty("card")]
        public CardPaymentMethod? Card { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; } = null!;
    }

    public partial class CardPaymentMethod 
    {
        [JsonProperty("brand")]
        public string Brand { get; set; } = null!;

        [JsonProperty("checks")]
        public Checks Checks { get; set; } = null!;

        [JsonProperty("country")]
        public string Country { get; set; } = null!;

        [JsonProperty("exp_month")]
        public long ExpMonth { get; set; }

        [JsonProperty("exp_year")]
        public long ExpYear { get; set; }

        [JsonProperty("fingerprint")]
        public string Fingerprint { get; set; } = null!;

        [JsonProperty("funding")]
        public string Funding { get; set; } = null!;

        [JsonProperty("installments")]
        public string? Installments { get; set; }

        [JsonProperty("last4")]
        public long Last4 { get; set; }

        [JsonProperty("mandate")]
        public string? Mandate { get; set; }

        [JsonProperty("network")]
        public string Network { get; set; } = null!;

        [JsonProperty("three_d_secure")]
        public object? ThreeDSecure { get; set; }

        [JsonProperty("wallet")]
        public object? Wallet { get; set; }
    }

    public partial class Checks
    {
        [JsonProperty("address_line1_check")]
        public string? AddressLine1Check { get; set; }

        [JsonProperty("address_postal_code_check")]
        public string? AddressPostalCodeCheck { get; set; }

        [JsonProperty("cvc_check")]
        public string? CvcCheck { get; set; }
    }
}
