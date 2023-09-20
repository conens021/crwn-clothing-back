using CrwnClothing.BLL.DTOs.Custom.Payments;
using CrwnClothing.BLL.DTOs.OrderDTOs;

namespace CrwnClothing.BLL.DTOs.Custom
{
    public class OrderIntentDTO : OrderDTO
    {
        public OrderIntentDTO(OrderDTO orderDTO) : base(orderDTO)
        {
        }

        public OrderIntentDTO(OrderDTO orderDTO,CcPaymenIntetntDTO ccPaymenIntetntDTO) : base(orderDTO)
        {
            PaymentIntent = ccPaymenIntetntDTO;
        }

        public CcPaymenIntetntDTO PaymentIntent { get; set; } = null!;
    }
}
