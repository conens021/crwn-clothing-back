using CrwnClothing.BLL.DTOs.Custom.Cart;
using CrwnClothing.BLL.DTOs.ShoppingCartDto;
using CrwnClothing.DAL.Entities;
using CrwnClothing.DAL.Models;
using System.Threading.Tasks;

namespace CrwnClothing.BLL.Mappers.ShoppingCartMappers
{
    public static class ShoppingCartMapper
    {
        public static ShoppingCart ToEntity(this ShoppingCartDTO shoppingCartDTO) => new() 
        {
            Id = shoppingCartDTO.Id,    
            CreatedAt = shoppingCartDTO.CreatedAt,
            UpdatedAt = shoppingCartDTO.UpdatedAt,  
            UserId = shoppingCartDTO.UserId
        };

        public static ShoppingCartDTO ToDTO(this ShoppingCart shoppingCart) => new()
        {
            Id = shoppingCart.Id,
            CreatedAt = shoppingCart.CreatedAt,
            UpdatedAt = shoppingCart.UpdatedAt,
            UserId = shoppingCart.UserId
        };

        public static ShoppingCart ToEntity(this CreateShoppingCartDTO createShoppingCartDTO) => new()
        {
            CreatedAt = DateTime.Now,
            UserId = createShoppingCartDTO.UserId
        };
    }
}
