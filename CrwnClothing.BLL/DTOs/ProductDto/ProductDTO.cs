using CrwnClothing.BLL.DTOs.CategoryDto;

namespace CrwnClothing.BLL.DTOs.ProductDto
{
    public class ProductDTO : BaseDTO
    {
        public string Name { get; set; } = null!;
        public string? ImageUrl { get; set; } = null!;
        public decimal Price { get; set; }
        public string? ContentId { get; set; }
        public int? ColorId { get; set; }
        public int? CategoryId { get; set; }
        public CategoryDTO? Category { get; set; } = null!;
    }
}
