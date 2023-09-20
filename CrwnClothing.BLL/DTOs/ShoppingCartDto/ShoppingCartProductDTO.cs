namespace CrwnClothing.BLL.DTOs.ShoppingCartDto
{
    public class ShoppingCartProductDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int ProductId { get; set; }
        public int ShoppingCartId { get; set; }
    }
}
