
using CrwnClothing.BLL.DTOs.Custom.Payments;
using CrwnClothing.BLL.DTOs.UserDto;

namespace CrwnClothing.BLL.Services.Payments
{
    public interface ICcPaymentService
    {
        public Task<CcPaymenIntetntDTO> CreatePaymentIntent(CreatePaymentIntentDTO createPaymentIntentDTO);
        public Task<CcPaymenIntetntDTO> CreatePaymentIntentWithCustomer(CreatePaymentIntentWithCustomerDTO createDTO);
        public Task<CcPaymenIntetntDTO> UpdatePaymentIntent(UpdatePaymentIntentDTO updatePaymentIntent);
        public Task<string> CreateCustomer(UserDTO user);
    }
}
