namespace CrwnClothing.BLL.DTOs.Custom.Payments
{
    public class CreatePaymentIntentWithCustomerDTO : CreatePaymentIntentDTO
    {
        public string CustomerId { get; set; } = null!;  
    }
}
