using CrwnClothing.DAL.Context;
using CrwnClothing.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrwnClothing.DAL.Repositories.ShoppingCartRepository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly CrwnClothingContext _context;

        public ShoppingCartRepository(CrwnClothingContext crwnClothingContext) : base(crwnClothingContext)
        {
            _context = crwnClothingContext;
        }

        public ShoppingCart? GetByUserId(int userId)
        {
            return _context.ShoppingCarts
                .AsNoTracking().
                Where(sc => sc.UserId == userId).FirstOrDefault();
        }

    }
}
