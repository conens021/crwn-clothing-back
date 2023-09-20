using CrwnClothing.BLL.DTOs.Custom.Payments;
using CrwnClothing.BLL.DTOs.OrderDTOs;
using CrwnClothing.BLL.DTOs.UserDto;
using Stripe;

namespace CrwnClothing.BLL.Mappers.Payments
{
    public static class CcPaymentMapper
    {
        public static CcPaymenIntetntDTO ToDTO(this PaymentIntent paymentIntent) => new()
        {
            Id = paymentIntent.Id,
            ClientSecret = paymentIntent.ClientSecret
        };

        public static PaymentIntentCreateOptions ToPaymeentIntentOptions(
            this CreatePaymentIntentDTO createPaymentIntent) => new()
            {
                Amount = createPaymentIntent.GetAmountInCents(),
                Currency = createPaymentIntent.Currency,
                Description = createPaymentIntent.Description,
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = createPaymentIntent.AutomaticPaymentMethods,
                },
                Metadata = createPaymentIntent?.Metadata,
                PaymentMethodTypes = createPaymentIntent?.PaymentMethodTypes
            };

        public static PaymentIntentCreateOptions ToPaymeentIntentOptions(
         this CreatePaymentIntentWithCustomerDTO createPaymentIntent) => new()
         {
             Customer = createPaymentIntent.CustomerId,
             Amount = createPaymentIntent.GetAmountInCents(),
             Currency = createPaymentIntent.Currency,
             Description = createPaymentIntent.Description,
             AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
             {
                 Enabled = createPaymentIntent.AutomaticPaymentMethods,
             },
             Metadata = createPaymentIntent?.Metadata,
             PaymentMethodTypes = createPaymentIntent?.PaymentMethodTypes
         };

        public static PaymentIntentUpdateOptions ToPaymeentIntentUpdateOptions(
            this UpdatePaymentIntentDTO updatePaymentIntent) => new()
            {
                Amount = updatePaymentIntent.GetAmountInCents(),
                Currency = updatePaymentIntent.Currency,
                Description = updatePaymentIntent.Description,
            };

        public static CreatePaymentIntentDTO ToCreatePaymentIntentDTO(this OrderDTO orderDTO) => new()
        {
            Amount = orderDTO.Total,
            Currency = "USD",
            Description = $"Payment intent for order #{orderDTO.Id} created!",
            AutomaticPaymentMethods = true,
            Metadata = new()
            {
                { "OrderId", orderDTO.Id.ToString() }
            },
        };

        public static CreatePaymentIntentWithCustomerDTO
            ToCreatePaymentIntentWithCustomerDTO(this OrderDTO orderDTO, string customerId) => new()
            {
                CustomerId = customerId,
                Amount = orderDTO.Total,
                Currency = "USD",
                Description = $"Payment intent for order #{orderDTO.Id} created!",
                AutomaticPaymentMethods = true,
                Metadata = new()
                {
                    { "OrderId", orderDTO.Id.ToString() }
                },
            };

        public static UpdatePaymentIntentDTO ToUpdatePaymentIntentDTO(this OrderDTO orderDTO, string id) => new()
        {
            Id = id,
            Amount = orderDTO.Total,
            Currency = "USD",
            Description = $"Payment intent for order #{orderDTO.Id} updated!",
        };

    }
}
