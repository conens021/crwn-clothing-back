
namespace CrwnClothing.BLL.External.Contracts.Stripe
{
    using System;
    using Newtonsoft.Json;

    public partial class PaymentIntentStripeEvent : BaseStripeEvent
    {
        [JsonProperty("data")]
        public new DataObject Data { get; set; } = null!;
    }

    public partial class DataObject
    {
        [JsonProperty("object")]
        public PaymentIntentObject PaymentIntent { get; set; } = null!;
    }

    public partial class PaymentIntentObject
    {
        [JsonProperty("id")]
        public string Id { get; set; } = null!;

        [JsonProperty("object")]
        public string ObjectName { get; set; } = null!;

        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("amount_capturable")]
        public long AmountCapturable { get; set; }

        [JsonProperty("amount_details")]
        public AmountDetails AmountDetails { get; set; } = null!;

        [JsonProperty("amount_received")]
        public long AmountReceived { get; set; }

        [JsonProperty("application")]
        public string? Application { get; set; }

        [JsonProperty("application_fee_amount")]
        public string? ApplicationFeeAmount { get; set; }

        [JsonProperty("automatic_payment_methods")]
        public AutomaticPaymentMethods AutomaticPaymentMethods { get; set; } = null!;

        [JsonProperty("canceled_at")]
        public TimeSpan? CanceledAt { get; set; }

        [JsonProperty("cancellation_reason")]
        public string? CancellationReason { get; set; }

        [JsonProperty("capture_method")]
        public string CaptureMethod { get; set; } = null!;

        [JsonProperty("charges")]
        public Charges Charges { get; set; } = null!;

        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; } = null!;

        [JsonProperty("confirmation_method")]
        public string ConfirmationMethod { get; set; } = null!;

        [JsonProperty("created")]
        public TimeSpan Created { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; } = null!;

        [JsonProperty("customer")]
        public object? Customer { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("invoice")]
        public object? Invoice { get; set; }

        [JsonProperty("last_payment_error")]
        public object? LastPaymentError { get; set; }

        [JsonProperty("livemode")]
        public bool Livemode { get; set; }

        [JsonProperty("metadata")]
        public IDictionary<string, string>? Metadata { get; set; }

        [JsonProperty("next_action")]
        public object? NextAction { get; set; }

        [JsonProperty("on_behalf_of")]
        public object? OnBehalfOf { get; set; }

        [JsonProperty("payment_method")]
        public string? PaymentMethod { get; set; }

        [JsonProperty("payment_method_options")]
        public PaymentMethodOptions PaymentMethodOptions { get; set; } = null!;

        [JsonProperty("payment_method_types")]
        public string[] PaymentMethodTypes { get; set; } = null!;

        [JsonProperty("processing")]
        public object? Processing { get; set; }

        [JsonProperty("receipt_email")]
        public object? ReceiptEmail { get; set; }

        [JsonProperty("review")]
        public object? Review { get; set; }

        [JsonProperty("setup_future_usage")]
        public object? SetupFutureUsage { get; set; }

        [JsonProperty("shipping")]
        public object? Shipping { get; set; }

        [JsonProperty("source")]
        public object? Source { get; set; }

        [JsonProperty("statement_descriptor")]
        public object? StatementDescriptor { get; set; }

        [JsonProperty("statement_descriptor_suffix")]
        public object? StatementDescriptorSuffix { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; } = null!;

        [JsonProperty("transfer_data")]
        public object? TransferData { get; set; }

        [JsonProperty("transfer_group")]
        public object? TransferGroup { get; set; }
    }

    public partial class AmountDetails
    {
        [JsonProperty("tip")]
        public Tip Tip { get; set; } = null!;
    }

    public partial class Tip
    {
        public int Amount { get; set; } = 0;
    }

    public partial class AutomaticPaymentMethods
    {
        [JsonProperty("enabled")]
        public bool Enabled { get; set; }
    }

    public partial class Charges
    {
        [JsonProperty("object")]
        public string Object { get; set; } = null!;

        [JsonProperty("data")]
        public object[]? Data { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("total_count")]
        public long TotalCount { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; } = null!;
    }

    public partial class PaymentMethodOptions
    {
        [JsonProperty("card")]
        public PaymentMethodOptionsCard Card { get; set; } = null!;

        [JsonProperty("link")]
        public PaymentMethodOptionsLink? Link { get; set; }
    }

    public partial class PaymentMethodOptionsLink 
    {
        [JsonProperty("persistent_token")]
        public string? PersistentToken { get; set; }
    }

    public partial class PaymentMethodOptionsCard
    {
        [JsonProperty("installments")]
        public object? Installments { get; set; }

        [JsonProperty("mandate_options")]
        public object? MandateOptions { get; set; }

        [JsonProperty("network")]
        public object? Network { get; set; }

        [JsonProperty("request_three_d_secure")]
        public string RequestThreeDSecure { get; set; } = null!;
    }
}
