using CrwnClothing.BLL.DTOs.Custom.Cart;
using CrwnClothing.BLL.DTOs.Custom.Payments;
using CrwnClothing.BLL.DTOs.OrderDTOs;

namespace CrwnClothing.BLL.DTOs.Custom
{
    public class OrderIntentWithCartProductsDTO : OrderIntentDTO
    {
        public OrderIntentWithCartProductsDTO(
            OrderDTO orderDTO, CcPaymenIntetntDTO paymentIntentDTO)
                : base(orderDTO, paymentIntentDTO)
        {
        }

        public OrderIntentWithCartProductsDTO(
            OrderDTO orderDTO,CcPaymenIntetntDTO  paymentIntentDTO,List<CartProductDTO> cartProducts) 
                :base(orderDTO,paymentIntentDTO) 
        {
            Products = cartProducts;
        }

        public List<CartProductDTO> Products { get; set; } = new List<CartProductDTO>();
    }
}
