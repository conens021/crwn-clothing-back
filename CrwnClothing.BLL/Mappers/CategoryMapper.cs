using CrwnClothing.BLL.DTOs;
using CrwnClothing.DAL.Entities;

namespace CrwnClothing.BLL.Mappers
{
    public static class CategoryMapper
    {
        public static Category ToEntity(this CategoryDTO categoryDTO) => new Category()
        {
            Id = categoryDTO.Id == null ? 0 : (int) categoryDTO.Id,
            Name = categoryDTO.Name,    
            CreatedAt = categoryDTO.CreatedAt == null ? DateTime.Now : (DateTime)categoryDTO.CreatedAt,
            UpdatedAt = categoryDTO.UpdatedAt == null ? DateTime.Now : (DateTime)categoryDTO.UpdatedAt,
        };

        public static CategoryDTO ToDTO(this Category category) => new CategoryDTO()
        {
            Id = category.Id,
            Name = category.Name,
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
