using CrwnClothing.BLL.DTOs.ProductDto;
using CrwnClothing.BLL.DTOs.SizesDTOs;

namespace CrwnClothing.BLL.DTOs.Custom.Cart
{
    public class CartProductDTO
    {
        public ProductDTO Product { get; set; } = new ProductDTO();
        public int Quantity { get; set; }
        public SizeDTO Size { get; set; } = new SizeDTO();
    }
}
