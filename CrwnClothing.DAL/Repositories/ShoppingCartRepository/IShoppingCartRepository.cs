
using CrwnClothing.DAL.Entities;
using CrwnClothing.DAL.Models;

namespace CrwnClothing.DAL.Repositories.ShoppingCartRepository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        public ShoppingCart? GetByUserId(int userId);
    }
}
