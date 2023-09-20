using CrwnClothing.DAL.Entities;

namespace CrwnClothing.DAL.Repositories.SizeRepository
{
    public interface IProductSizeRepository : IRepository<ProductsSize>
    {
        public IQueryable<ProductsSize> GetAvailableSizesByProductId(int productId);
        public ProductsSize? GetByProductIdAndSizeId(int productId,int sizeId);
    }
}
