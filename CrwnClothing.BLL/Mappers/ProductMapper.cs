using CrwnClothing.BLL.DTOs.Custom.Cart;
using CrwnClothing.BLL.DTOs.OrderDTOs;
using CrwnClothing.BLL.DTOs.ProductDto;
using CrwnClothing.BLL.Mappers.SizeMappers;
using CrwnClothing.DAL.Entities;

namespace CrwnClothing.BLL.Mappers
{
    public static class ProductMapper
    {
        public static Product ToEntity(this ProductDTO productDto) => new()
        {
            Id = productDto.Id,
            Name = productDto.Name,
            Price = productDto.Price,
            ImageUrl = productDto.ImageUrl,
            ContentId = productDto.ContentId,
            ColorId = productDto.ColorId,
            CreatedAt = productDto.CreatedAt,
            UpdatedAt = productDto.UpdatedAt,
            CategoryId = productDto.CategoryId
        };

        public static Product ToEntity(this CreateProductDTO productDto) => new()
        {
            Name = productDto.Name,
            Price = productDto.Price,
            ColorId = productDto.ColorId,
            CategoryId = productDto.CategoryId,
            CreatedAt = DateTime.Now
        };


        public static CartProductDTO ToCartProductDTO(this ProductDTO productDto, int quantity) => new()
        {
            Product = productDto,
            Quantity = quantity
        };

        public static CartProductDTO ToCartProductDTO(this ShoppingCartProductWithProductAndSizeDTO dto) => new()
        {
            Product = dto.Product,
            Quantity = dto.Quantity,
            Size = dto.Size
        };

        public static List<CartProductDTO> ToCartProductsDTO(this
            List<ShoppingCartProductWithProductAndSizeDTO> shoppingCartProducts)
        {

            return shoppingCartProducts.Select(scp => scp.ToCartProductDTO()).ToList();
        }

        public static ProductDTO ToDTO(this Product product) => new()
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            ContentId = product.ContentId,
            CategoryId = product.CategoryId,
            ColorId = product.ColorId,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt
        };

        public static ProductDTO ToDTOWithCategory(this Product product) => new()
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            ColorId = product.ColorId,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt,
            Category = product?.Category?.ToDTO()
        };

        public static ProductWithCategoryAndSizesDTO ToDTOWithCategoryAndSizes(this Product product) => new()
        {
            Product = product.ToDTO(),
            Category = product.Category?.ToDTO(),
            Sizes = product.ProductsSizes.Select(ps => ps.Size.ToDTO()).ToList()
        };
    }
}
