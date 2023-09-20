
using CrwnClothing.BLL.Utils;

namespace CrwnClothing.BLL.DTOs.Custom.Payments
{
    public class UpdatePaymentIntentDTO
    {
        public string Id { get; set; } = null!;
        public decimal Amount { get; set; }
        public string? Currency { get; set; }
        public string Description { get; set; } = "Payment Intent Updated!";


        public long GetAmountInCents()
        {
            return CurrencyUtils.ConvertDollarsToCents(Amount);
        }
    }
}
