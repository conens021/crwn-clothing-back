using CrwnClothing.BLL.Contracts.Enums;
using CrwnClothing.BLL.DTOs.Custom;
using CrwnClothing.BLL.DTOs.OrderDTOs;
using CrwnClothing.BLL.DTOs.UserDto;

namespace CrwnClothing.BLL.Services.OrderService
{
    public interface IOrderService : IBaseService<OrderDTO, CreateOrderDTO>
    {
        public Task<OrderDTO> Update(int orderId, OrderShippingDetailsDTO shippingDetailsDTO);
        public Task<OrderIntentWithCartProductsDTO> CreateOrderIntent(UserDTO user);
        public Task<OrderIntentWithCartProductsDTO> UpdateOrderIntent(UserDTO userDTO, int orderId);
        public Task<OrderWithProductsDTO> CreateFromUser(UserDTO userDTO);
        public Task<OrderTotalWithProductsDTO> GetOrderTotalFromUserCartWithProducts(UserDTO userDTO);
        public Task<OrderDTO> StartOrderRequest(int id, UserDTO user);
        public Task<OrderDTO> OrderRequestFailed(int id, UserDTO user);
        public Task UpdatePaymentStats(int orderId, PaymentStatus paymentStatus);
        public Task<OrderDTO> UpdateTotalChaarged(int orderId, decimal totalCharged);
    }
}
