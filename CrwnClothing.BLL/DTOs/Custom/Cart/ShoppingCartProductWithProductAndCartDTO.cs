using CrwnClothing.BLL.DTOs.ProductDto;
using CrwnClothing.BLL.DTOs.ShoppingCartDto;

namespace CrwnClothing.BLL.DTOs.Custom.Cart
{
    public class ShoppingCartProductWithProductAndCartDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int ProductId { get; set; }
        public int ShoppingCartId { get; set; }
        public ProductDTO Product { get; set; } = new ProductDTO();
        public ShoppingCartDTO ShoppingCart { get; set; } = new ShoppingCartDTO();
    }
}
