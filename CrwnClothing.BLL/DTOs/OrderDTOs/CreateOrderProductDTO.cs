namespace CrwnClothing.BLL.DTOs.OrderDTOs
{
    public class CreateOrderProductDTO
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public int SizeId { get; set; }
    }
}
