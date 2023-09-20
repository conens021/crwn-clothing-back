using CrwnClothing.BLL.DTOs.CategoryDto;
using CrwnClothing.BLL.DTOs.SizesDTOs;

namespace CrwnClothing.BLL.DTOs.ProductDto
{
    public class ProductWithCategoryAndSizesDTO
    {
        public ProductDTO Product { get; set; } = null!;
        public CategoryDTO? Category { get; set; } = null!;
        public List<SizeDTO> Sizes { get; set; } = new List<SizeDTO>();
    }
}
