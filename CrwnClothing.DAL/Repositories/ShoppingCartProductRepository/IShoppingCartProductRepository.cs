
using CrwnClothing.DAL.Entities;

namespace CrwnClothing.DAL.Repositories.ShoppingCartProductRepository
{
    public interface IShoppingCartProductRepository : IRepository<ShoppingCartProduct>
    {
        public IEnumerable<ShoppingCartProduct> GetByShoppingCartId(int cartId);
        public ShoppingCartProduct? GetByProductIdAndShoppingCartId(int productId, int shoppingCartId);
        public IEnumerable<ShoppingCartProduct> RemoveByCartId(int cartId);

    }
}
