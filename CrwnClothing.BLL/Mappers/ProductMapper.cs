using CrwnClothing.BLL.DTOs;
using CrwnClothing.DAL.Entities;

namespace CrwnClothing.BLL.Mappers
{
    public static class ProductMapper
    {
        public static Product ToEntity(this ProductDTO productDto) => new()
        {
            Id = productDto.Id == null ? 0 : (int)productDto.Id,
            Name = productDto.Name,
            Price = productDto.Price,
            ImageUrl = productDto.ImageUrl,
            CreatedAt = productDto.CreatedAt,
            UpdatedAt = productDto.UpdatedAt,
            CategoryId = productDto.CategoryId
        };

        public static ProductDTO ToDTO(this Product product) => new()
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt
        };

        public static ProductDTO ToDTOWithCategory(this Product product) => new()
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt,
            Category = product?.Category?.ToDTO()
        };

    }
}
