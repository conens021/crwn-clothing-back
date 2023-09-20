using CrwnClothing.BLL.DTOs.ProductDto;
using CrwnClothing.BLL.DTOs.SizesDTOs;

namespace CrwnClothing.BLL.DTOs.OrderDTOs
{
    public class ShoppingCartProductWithProductAndSizeDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public int ShoppingCartId { get; set; }
        public ProductDTO Product { get; set; } = new ProductDTO();
        public SizeDTO Size { get; set; } = new SizeDTO();
    }
}
