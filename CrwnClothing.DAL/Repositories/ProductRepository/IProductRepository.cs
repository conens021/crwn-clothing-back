using CrwnClothing.DAL.Entities;

namespace CrwnClothing.DAL.Repositories.ProductRepository
{
    public interface IProductRepository
    {
        public Product? Get(int id);
        public IEnumerable<Product> GetAll();
        public Product CreateProduct(Product product);
        public Product UpdateProduct(Product product);
        public Product DeleteProduct(Product product);
    }
}
