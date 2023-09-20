using CrwnClothing.BLL.DTOs.CategoryDto;
using CrwnClothing.DAL.Entities;

namespace CrwnClothing.BLL.Mappers
{
    public static class CategoryMapper
    {
        public static Category ToEntity(this CategoryDTO categoryDTO) => new Category()
        {
            Id = categoryDTO.Id,
            Name = categoryDTO.Name,    
            CoverImageUrl = categoryDTO.CoverImageUrl,
            CreatedAt = categoryDTO.CreatedAt,
            UpdatedAt = categoryDTO.UpdatedAt
        };

        public static Category ToEntity(this CreateCategoryDTO categoryDTO) => new Category()
        {
            Name = categoryDTO.Name,
            CreatedAt = DateTime.Now
        };

        public static CategoryDTO ToDTO(this Category category) => new CategoryDTO()
        {
            Id = category.Id,
            Name = category.Name,
            CoverImageUrl = category.CoverImageUrl,
            CreatedAt = category.CreatedAt,
            UpdatedAt = category.UpdatedAt
        };

        public static CategoryDTO ToDTOWithProducts(this Category category) => new CategoryDTO()
        {
            Id = category.Id,
            Name = category.Name,
            CreatedAt = category.CreatedAt,
            UpdatedAt = category.UpdatedAt,
            Products = category.Products.Select(p => p.ToDTO()).ToList()
        };
    }
}
