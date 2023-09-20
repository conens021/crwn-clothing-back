using CrwnClothing.BLL.DTOs.Custom.Cart;

namespace CrwnClothing.BLL.DTOs.OrderDTOs
{
    public class OrderWithProductsDTO : OrderDTO
    {
        public OrderWithProductsDTO(OrderDTO parent) : base(parent)
        {
        }

        public OrderWithProductsDTO(OrderDTO parent, List<CartProductDTO> cartProducts) : base(parent)
        {
            Products = cartProducts;
        }

        public List<CartProductDTO> Products { get; set; } = null!;
    }
}
