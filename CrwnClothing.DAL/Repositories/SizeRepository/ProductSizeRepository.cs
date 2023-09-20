using CrwnClothing.DAL.Context;
using CrwnClothing.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrwnClothing.DAL.Repositories.SizeRepository
{
    public class ProductSizeRepository : Repository<ProductsSize>, IProductSizeRepository
    {
        private readonly CrwnClothingContext _context;

        public ProductSizeRepository(CrwnClothingContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<ProductsSize> GetAvailableSizesByProductId(int productId)
        {
            return _context.ProductsSizes
                    .Where(ps => ps.ProductId == productId && ps.QuantityAvailable >= 1);
        }

        public ProductsSize? GetByProductIdAndSizeId(int productId, int sizeId)
        {
            return _context.ProductsSizes
                .Where(ps =>
                        ps.ProductId == productId
                        && ps.SizeId == sizeId)
                .FirstOrDefault();
        }
    }
}
