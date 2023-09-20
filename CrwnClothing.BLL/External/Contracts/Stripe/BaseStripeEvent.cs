namespace CrwnClothing.BLL.External.Contracts.Stripe
{
    using System;
    using Newtonsoft.Json;

    public partial class BaseStripeEvent
    {
        [JsonProperty("id")]
        public string Id { get; set; } = null!;

        [JsonProperty("object")]
        public string ObjectType { get; set; } = null!;

        [JsonProperty("api_version")]
        public string ApiVersion { get; set; } = null!;

        [JsonProperty("created")]
        public long Created { get; set; }

        [JsonProperty("data")]
        public object Data { get; set; } = null!;

        [JsonProperty("livemode")]
        public bool Livemode { get; set; }

        [JsonProperty("pending_webhooks")]
        public long PendingWebhooks { get; set; }

        [JsonProperty("request")]
        public Request Request { get; set; } = null!;

        [JsonProperty("type")]
        public string EventType { get; set; } = null!;
    }

    public partial class Request
    {
        [JsonProperty("id")]
        public string Id { get; set; } = null!;

        [JsonProperty("idempotency_key")]
        public string IdempotencyKey { get; set; } = null!;
    }
}

