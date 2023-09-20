
using CrwnClothing.BLL.DTOs.Custom.Cart;
using CrwnClothing.BLL.DTOs.OrderDTOs;
using CrwnClothing.BLL.DTOs.ShoppingCartDto;
using CrwnClothing.BLL.Mappers.SizeMappers;
using CrwnClothing.DAL.Entities;

namespace CrwnClothing.BLL.Mappers.ShoppingCartMappers
{
    public static class ShoppingCartProductMapper
    {
        public static ShoppingCartProductDTO ToDTO(this ShoppingCartProduct entity) => new()
        {
            Id = entity.Id,
            Quantity = entity.Quantity,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            ProductId = entity.ProductId,
            ShoppingCartId = entity.ShoppingCartId,
        };

        public static ShoppingCartProductWithProductDTO ToDTOWithProduct(this ShoppingCartProduct entity) => new()
        {
            Id = entity.Id,
            Quantity = entity.Quantity,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            ProductId = entity.ProductId,
            ShoppingCartId = entity.ShoppingCartId,
            Product = entity.Product.ToDTO()
        };

        public static ShoppingCartProductWithProductAndSizeDTO ToDTOWithProductAndSize
            (this ShoppingCartProduct entity) => new()
            {
                Id = entity.Id,
                Quantity = entity.Quantity,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                ProductId = entity.ProductId,
                ShoppingCartId = entity.ShoppingCartId,
                Product = entity.Product.ToDTO(),
                SizeId = entity.SizeId,
                Size = entity.Size.ToDTO(),
            };

        public static ShoppingCartProductWithProductAndCartDTO
            ToDTOWithProductAndShoppingCart(this ShoppingCartProduct entity) => new()
            {
                Id = entity.Id,
                Quantity = entity.Quantity,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                ProductId = entity.ProductId,
                ShoppingCartId = entity.ShoppingCartId,
                Product = entity.Product.ToDTO(),
                ShoppingCart = entity.ShoppingCart.ToDTO()
            };

        public static ShoppingCartProduct ToEntity(this ShoppingCartProductDTO dto) => new()
        {
            Id = dto.Id,
            Quantity = dto.Quantity,
            CreatedAt = dto.CreatedAt,
            UpdatedAt = dto.UpdatedAt,
            ProductId = dto.ProductId,
            ShoppingCartId = dto.ShoppingCartId,
        };

        public static ShoppingCartProduct ToEntity(this CreateShoppingCartProductDTO dto) => new()
        {
            Quantity = dto.Quantity,
            CreatedAt = DateTime.Now,
            ProductId = dto.ProductId,
            ShoppingCartId = dto.ShoppingCartId,
        };

        public static ShoppingCartProduct ToEntity(this ShoppingCartProductWithProductDTO dto) => new()
        {
            Id = dto.Id,
            Quantity = dto.Quantity,
            CreatedAt = dto.CreatedAt,
            UpdatedAt = dto.UpdatedAt,
            ProductId = dto.ProductId,
            SizeId = dto.SizeId,
            ShoppingCartId = dto.ShoppingCartId,
        };

        public static ShoppingCartProduct ToEntity(this ShoppingCartProductWithProductAndSizeDTO dto) => new()
        {
            Id = dto.Id,
            Quantity = dto.Quantity,
            CreatedAt = dto.CreatedAt,
            UpdatedAt = dto.UpdatedAt,
            ProductId = dto.ProductId,
            SizeId = dto.SizeId,
            ShoppingCartId = dto.ShoppingCartId,
        };

        public static ShoppingCartProduct ToEntityWithProduct(this ShoppingCartProductWithProductDTO dto) => new()
        {
            Id = dto.Id,
            Quantity = dto.Quantity,
            CreatedAt = dto.CreatedAt,
            UpdatedAt = dto.UpdatedAt,
            ProductId = dto.ProductId,
            ShoppingCartId = dto.ShoppingCartId,
            Product = dto.Product.ToEntity()
        };

        public static CartProductDTO ToCartProductDTO(this ShoppingCartProductWithProductDTO dto) => new()
        {
            Product = dto.Product,
            Quantity = dto.Quantity
        };

        public static CartProductDTO ToCartProductDTO(this ShoppingCartProductWithProductAndSizeDTO dto) => new()
        {
            Product = dto.Product,
            Quantity = dto.Quantity,
            Size = dto.Size
        };

    }
}
