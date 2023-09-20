using CrwnClothing.BLL.DTOs.ProductDto;
using CrwnClothing.BLL.DTOs.ShoppingCartDto;

namespace CrwnClothing.BLL.DTOs.Custom.Cart
{
    public class ShoppingCartWithProductsDTO
    {
        public ShoppingCartDTO ShoppingCart { get; set; } = new ShoppingCartDTO();
        public List<CartProductDTO> Products { get; set; } = new List<CartProductDTO>();
    }
}
