using CrwnClothing.BLL.DTOs.ProductDto;

namespace CrwnClothing.BLL.DTOs.Custom.Cart
{
    public class ShoppingCartProductWithProductDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int ProductId { get; set; }
        public int ShoppingCartId { get; set; }
        public int SizeId { get; set; }
        public ProductDTO Product { get; set; } = new ProductDTO();
    }
}
