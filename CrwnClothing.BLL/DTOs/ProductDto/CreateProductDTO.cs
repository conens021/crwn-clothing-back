namespace CrwnClothing.BLL.DTOs.ProductDto
{
    public class CreateProductDTO
    {
        public string Name { get; set; } = null!;
        public string? Image { get; set; }
        public decimal Price { get; set; }
        public int? ColorId { get; set; }
        public int? CategoryId { get; set; }
    }
}
