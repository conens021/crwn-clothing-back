using CrwnClothing.DAL.Context;
using CrwnClothing.DAL.Entities;

namespace CrwnClothing.DAL.Repositories.ProductRepository
{
    public class ProductRepository : IProductRepository
    {

        private readonly CrwnClothingContext _context;

        public ProductRepository(CrwnClothingContext crwnClothingContext)
        {
            _context = crwnClothingContext;
        }

        public Product CreateProduct(Product product)
        {
            _context.Products.Add(product);

            _context.SaveChanges();

            return product;
        }

        public Product DeleteProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Product? Get(int id)
        {
            return _context.Products.Where(product => product.Id == id).FirstOrDefault();
        }

        public Product UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products;
        }
    }
}
