namespace CrwnClothing.BLL.External.Contracts.Stripe
{
    using System;
    using Newtonsoft.Json;

    public partial class ChargeStripeEvent : BaseStripeEvent
    {
        [JsonProperty("data")]
        public new DataObject Data { get; set; } = null!;
    }

    public partial class DataObject
    {
        [JsonProperty("object")]
        public ChargeIntentObject Charge { get; set; } = null!;
    }

    public partial class ChargeIntentObject
    {
        [JsonProperty("id")]
        public string Id { get; set; } = null!;

        [JsonProperty("object")]
        public string ObjectName { get; set; } = null!;

        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("amount_captured")]
        public long AmountCaptured { get; set; }

        [JsonProperty("amount_refunded")]
        public long AmountRefunded { get; set; }

        [JsonProperty("application")]
        public string? Application { get; set; }

        [JsonProperty("application_fee")]
        public string? ApplicationFee { get; set; }

        [JsonProperty("application_fee_amount")]
        public string? ApplicationFeeAmount { get; set; }

        [JsonProperty("balance_transaction")]
        public string BalanceTransaction { get; set; } = null!;

        [JsonProperty("billing_details")]
        public BillingDetails BillingDetails { get; set; } = null!;

        [JsonProperty("calculated_statement_descriptor")]
        public string CalculatedStatementDescriptor { get; set; } = null!;

        [JsonProperty("captured")]
        public bool Captured { get; set; }

        [JsonProperty("created")]
        public TimeSpan Created { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; } = null!;

        [JsonProperty("customer")]
        public object? Customer { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("destination")]
        public object? Destination { get; set; }

        [JsonProperty("dispute")]
        public object? Dispute { get; set; }

        [JsonProperty("disputed")]
        public bool Disputed { get; set; }

        [JsonProperty("failure_balance_transaction")]
        public object? FailureBalanceTransaction { get; set; }

        [JsonProperty("failure_code")]
        public string? FailureCode { get; set; }

        [JsonProperty("failure_message")]
        public string? FailureMessage { get; set; }

        [JsonProperty("fraud_details")]
        public FraudDetails? FraudDetails { get; set; }

        [JsonProperty("invoice")]
        public string? InvoiceId { get; set; }

        [JsonProperty("livemode")]
        public bool Livemode { get; set; }

        [JsonProperty("metadata")]
        public IDictionary<string,string>? Metadata { get; set; }

        [JsonProperty("on_behalf_of")]
        public object? OnBehalfOf { get; set; }

        [JsonProperty("order")]
        public object? Order { get; set; }

        [JsonProperty("outcome")]
        public Outcome Outcome { get; set; } = null!;

        [JsonProperty("paid")]
        public bool Paid { get; set; }

        [JsonProperty("payment_intent")]
        public string? PaymentIntentId { get; set; } = null!;

        [JsonProperty("payment_method")]
        public string PaymentMethodId { get; set; } = null!;

        [JsonProperty("payment_method_details")]
        public StripePaymentMethodDetails PaymentMethodDetails { get; set; } = null!;

        [JsonProperty("receipt_email")]
        public string? ReceiptEmail { get; set; }

        [JsonProperty("receipt_number")]
        public string? ReceiptNumber { get; set; }

        [JsonProperty("receipt_url")]
        public string? ReceiptUrl { get; set; }

        [JsonProperty("refunded")]
        public bool Refunded { get; set; }

        [JsonProperty("refunds")]
        public Refunds Refunds { get; set; } = null!;

        [JsonProperty("review")]
        public string? Review { get; set; }

        [JsonProperty("shipping")]
        public object? Shipping { get; set; }

        [JsonProperty("source")]
        public object? Soruce { get; set; }

        [JsonProperty("source_transfer")]
        public string? SourceTransferId { get; set; }

        [JsonProperty("statement_descriptor")]
        public string? StatementDescriptor { get; set; }

        [JsonProperty("statement_descriptor_suffix")]
        public string? StatementDescriptorSuffix { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; } = null!;

        [JsonProperty("transfer_data")]
        public object? TransferData { get; set; }

        [JsonProperty("transfer_group")]
        public string? TransferGroup { get; set; }
    }

    public partial class BillingDetails
    {
        [JsonProperty("address")]
        public Address Address { get; set; } = null!;

        [JsonProperty("email")]
        public string? Email { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("phone")]
        public string? Phone { get; set; }
    }

    public partial class Address
    {
        [JsonProperty("city")]
        public string? City { get; set; }

        [JsonProperty("country")]
        public string? Country { get; set; }

        [JsonProperty("line1")]
        public string? Line1 { get; set; }

        [JsonProperty("line2")]
        public string? Line2 { get; set; }

        [JsonProperty("postal_code")]
        public string? PostalCode { get; set; }

        [JsonProperty("state")]
        public string? State { get; set; }
    }

    public partial class FraudDetails
    {
        [JsonProperty("stripe_report")]
        public string? StripeReposrt { get; set; }

        [JsonProperty("user_report")]
        public string? UserReport { get; set; }
    }


    public partial class Outcome
    {
        [JsonProperty("network_status")]
        public string NetworkStatus { get; set; } = null!;

        [JsonProperty("reason")]
        public string? Reason { get; set; }

        [JsonProperty("risk_level")]
        public string RiskLevel { get; set; } = null!;

        [JsonProperty("risk_score")]
        public long RiskScore { get; set; }

        [JsonProperty("seller_message")]
        public string SellerMessage { get; set; } = null!;

        [JsonProperty("type")]
        public string Type { get; set; } = null!;
    }


    public partial class Refunds
    {
        [JsonProperty("object")]
        public string ObjectName { get; set; } = null!;

        [JsonProperty("data")]
        public object[]? Data { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("total_count")]
        public long TotalCount { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; } = null!;
    }
}
