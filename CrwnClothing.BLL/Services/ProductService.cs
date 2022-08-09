
using CrwnClothing.BLL.DTOs;
using CrwnClothing.DAL.Repositories.ProductRepository;
using CrwnClothing.BLL.Mappers;
using CrwnClothing.DAL.Entities;
using DroneDropshipping.BLL.Exceptions;

namespace CrwnClothing.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;


        public ProductService(IProductRepository productRepository)
        { 
            _productRepository = productRepository;
        }

        public ProductDTO CreateProduct(ProductDTO product)
        {
            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;

            Product createdProduct = _productRepository.CreateProduct(product.ToEntity());


            return createdProduct.ToDTO();
        }

        public ProductDTO DeleteProduct(ProductDTO product)
        {
            throw new NotImplementedException();
        }

        public List<ProductDTO> GetAll()
        {
            return _productRepository.GetAll().Select(p => p.ToDTO()).ToList();
        }

        public ProductDTO GetProduct(int id)
        {
            Product? product = _productRepository.Get(id);

            if (product == null) throw new BusinessException("Product not Found",404);


            return product.ToDTO();
        }

        public ProductDTO UpdateProduct(ProductDTO product)
        {
            throw new NotImplementedException();
        }
    }
}
