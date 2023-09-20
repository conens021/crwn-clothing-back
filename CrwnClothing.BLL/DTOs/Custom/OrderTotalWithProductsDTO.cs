using CrwnClothing.BLL.DTOs.Custom.Cart;

namespace CrwnClothing.BLL.DTOs.Custom
{
    public class OrderTotalWithProductsDTO
    {
        public decimal Total { get; set; }
        public decimal Subtotal { get; set; }
        public List<CartProductDTO> CartProducts { get; set; } = new List<CartProductDTO>();
    }
}
