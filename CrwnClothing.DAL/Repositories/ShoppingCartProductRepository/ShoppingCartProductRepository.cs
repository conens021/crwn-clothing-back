
using CrwnClothing.DAL.Context;
using CrwnClothing.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace CrwnClothing.DAL.Repositories.ShoppingCartProductRepository
{
    public class ShoppingCartProductRepository : Repository<ShoppingCartProduct>, IShoppingCartProductRepository
    {
        private readonly CrwnClothingContext _context;

        public ShoppingCartProductRepository(CrwnClothingContext context) : base(context)
        {
            _context = context;
        }

        public ShoppingCartProduct? GetByProductIdAndShoppingCartId(int productId, int shoppingCartId)
        {
            return _context.ShoppingCartProducts
                .AsNoTracking()
                .Where(scp => scp.ShoppingCartId == shoppingCartId && scp.ProductId == productId)
                .FirstOrDefault();
        }

        public IEnumerable<ShoppingCartProduct> GetByShoppingCartIdWithProduct(int shoppingCartId)
        {
            return _context.ShoppingCartProducts
                .AsNoTracking()
                .Include(scp => scp.Product)
                .Where(scp => scp.ShoppingCartId == shoppingCartId);
        }

        public IEnumerable<ShoppingCartProduct> GetByShoppingCartIdWithProductAndSize(int shoppingCartId)
        {
            return _context.ShoppingCartProducts
                .AsNoTracking()
                .Include(scp => scp.Product)
                .Include(scp => scp.Size)
                .Where(scp => scp.ShoppingCartId == shoppingCartId);
        }

        public IEnumerable<ShoppingCartProduct> GetByShoppingCartId(int cartId)
        {
            return _context.ShoppingCartProducts.AsNoTracking().Where(scp => scp.ShoppingCartId == cartId);
        }

        public IEnumerable<ShoppingCartProduct> RemoveByCartId(int cartId)
        {
            IQueryable<ShoppingCartProduct> shoppingCartProducts =
                _context.ShoppingCartProducts
                .AsNoTracking()
                .IgnoreAutoIncludes()
                .Where(scp => scp.ShoppingCartId == cartId);

            _context.RemoveRange(shoppingCartProducts);

            _context.SaveChanges();


            return shoppingCartProducts.AsEnumerable();
        }
    }
}
