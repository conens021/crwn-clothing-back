
using Microsoft.Extensions.Configuration;
using Stripe;
using CrwnClothing.BLL.DTOs.Custom.Payments;
using CrwnClothing.BLL.Mappers.Payments;
using DroneDropshipping.BLL.Exceptions;
using CrwnClothing.BLL.DTOs.UserDto;

namespace CrwnClothing.BLL.Services.Payments
{
    public class CcPaymentService : ICcPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IStripeClient _stripeClient;

        public CcPaymentService(IConfiguration configuration)
        {
            _configuration = configuration;
            this._stripeClient = new StripeClient(_configuration["Stripe:SecretKey"]);
        }

        public async Task<CcPaymenIntetntDTO> CreatePaymentIntent(CreatePaymentIntentDTO createPaymentIntent)
        {
            var options = createPaymentIntent.ToPaymeentIntentOptions();

            try
            {
                var service = new PaymentIntentService(this._stripeClient);

                var paymentIntent = await service.CreateAsync(options);


                return paymentIntent.ToDTO();
            }
            catch (StripeException ex)
            {
                throw new BusinessException(ex.Message, 400);
            }
        }

        public async Task<CcPaymenIntetntDTO> CreatePaymentIntentWithCustomer
            (CreatePaymentIntentWithCustomerDTO createPaymentIntent)
        {
            var options = createPaymentIntent.ToPaymeentIntentOptions();

            try
            {
                var service = new PaymentIntentService(this._stripeClient);

                var paymentIntent = await service.CreateAsync(options);


                return paymentIntent.ToDTO();
            }
            catch (StripeException ex)
            {
                throw new BusinessException(ex.Message, 400);
            }
        }

        public async Task<CcPaymenIntetntDTO> UpdatePaymentIntent(UpdatePaymentIntentDTO updatePaymentIntent)
        {
            var options = updatePaymentIntent.ToPaymeentIntentUpdateOptions();

            try
            {
                var service = new PaymentIntentService(this._stripeClient);

                var paymentIntent = await service.UpdateAsync(updatePaymentIntent.Id, options);


                return paymentIntent.ToDTO();
            }
            catch (StripeException ex)
            {
                throw new BusinessException(ex.Message, 400);
            }
        }

        public async Task<string> CreateCustomer(UserDTO user)
        {
            var options = new CustomerCreateOptions
            {
                Phone = user.PhoneNumber,
                Email = user.Email,
                Name = $"{user.FirstName} {user.LastName}",
                Description = "My First Test Customer",
            };

            try
            {
                var service = new CustomerService(this._stripeClient);

                Customer customer = await service.CreateAsync(options);

                return customer.Id;
            }
            catch (StripeException ex)
            {
                throw new BusinessException(ex.Message, 400);
            }


        }
    }
}
