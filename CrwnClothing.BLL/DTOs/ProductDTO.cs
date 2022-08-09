namespace CrwnClothing.BLL.DTOs
{
    public class ProductDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? CategoryId { get; set; }
        public CategoryDTO? Category { get; set; }  = null!;
    }
}
