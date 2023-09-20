using CrwnClothing.BLL.Utils;
using Stripe;

namespace CrwnClothing.BLL.DTOs.Custom.Payments
{
    public class CreatePaymentIntentDTO
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "USD";
        public string Description { get; set; } = string.Empty;
        public Boolean AutomaticPaymentMethods { get; set; } = true;
        public List<string>? PaymentMethodTypes { get; set; }
        public Dictionary<string, string>? Metadata { get; set; }


        public long GetAmountInCents() 
        {

            return CurrencyUtils.ConvertDollarsToCents(this.Amount);
        }
    }
}
