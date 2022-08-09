
using CrwnClothing.BLL.DTOs;

namespace CrwnClothing.BLL.Services
{
    public interface IProductService
    {
        public ProductDTO GetProduct(int id);
        public ProductDTO CreateProduct(ProductDTO product);
        public ProductDTO UpdateProduct(ProductDTO product);
        public ProductDTO DeleteProduct(ProductDTO product);
        public List<ProductDTO> GetAll();
    }
}
