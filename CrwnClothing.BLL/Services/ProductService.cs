
using CrwnClothing.BLL.DTOs.ProductDto;
using CrwnClothing.DAL.Repositories.ProductRepository;
using CrwnClothing.BLL.Mappers;
using CrwnClothing.DAL.Entities;
using DroneDropshipping.BLL.Exceptions;
using CrwnClothing.BLL.Helpers;
using CrwnClothing.BLL.DTOs.FiltersDTO;
using CrwnClothing.BLL.Mappers.FilteringMappers;
using CrwnClothing.BLL.DTOs;
using CrwnClothing.BLL.DTOs.SortingDto;

namespace CrwnClothing.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDTO> Create(CreateProductDTO productDTO)
        {
            Product product = productDTO.ToEntity();

            if (productDTO.Image != null)
            {
                string folderName = GenerateProductContentFolder();

                string fileName =
                    await UploadImage(productDTO.Image, folderName);

                product.ImageUrl = fileName;
                product.ContentId = folderName;
            }

            Product created = await _productRepository.Create(product);


            return created.ToDTO();
        }

        public Task<ProductDTO> Delete(ProductDTO product)
        {
            throw new NotImplementedException();
        }

        public List<ProductDTO> GetAll()
        {
            return _productRepository.GetAll().Select(p => p.ToDTO()).ToList();
        }

        public ProductDTO? GetById(int id)
        {
            Product? product = _productRepository.GetById(id);


            return product?.ToDTO();
        }

        public ProductDTO GetSafeById(int id)
        {
            Product? product = _productRepository.GetById(id);

            if (product == null) throw new BusinessException("Product not Found", 404);


            return product.ToDTO();
        }

        public async Task<ProductDTO> Update(ProductDTO product)
        {
            Product updated = await _productRepository.Update(product.ToEntity());


            return updated.ToDTO();
        }

        public List<ProductDTO> GetByIdBulk(List<int> ids)
        {
            return _productRepository
                    .GetAll(p => ids.Contains(p.Id))
                    .Select(p => p.ToDTO())
                    .ToList();
        }

        public List<ProductDTO> GetAll(PaginationDTO paginationDTO)
        {

            return _productRepository
                .GetAll(paginationDTO.ToEntity())
                .Select(product => product.ToDTO())
                .ToList();
        }


        public List<ProductDTO> GetAll(
            PaginationDTO paginationDTO,
            SortingDTO sorting)
        {
            return _productRepository
                .GetAll(paginationDTO.ToEntity(), sorting.ToEntity())
                .Select(product => product.ToDTO())
                .ToList();
        }

        public List<ProductDTO> GetAll(
            PaginationDTO paginationDTO,
            ProductFilterDTO productFilterDTO)
        {

            return _productRepository
                .GetAll(paginationDTO.ToEntity(), productFilterDTO.ToEntity())
                .Select(product => product.ToDTO())
                .ToList();
        }

        public List<ProductDTO> GetAll(
            PaginationDTO paginationDTO,
            SortingDTO sortingDTO,
            ProductFilterDTO productFilterDTO)
        {
            return _productRepository
                .GetAll(paginationDTO.ToEntity(),
                        sortingDTO.ToEntity(),
                        productFilterDTO.ToEntity())
                .Select(product => product.ToDTO())
                .ToList();
        }


        public List<ProductDTO> GetAllByCategory(
            string categoryName,
            PaginationDTO paginationDTO,
            SortingDTO sortingDTO, ProductFilterDTO productFilterDTO)
        {
            return _productRepository
               .GetAllByCategory(
                    categoryName,
                    paginationDTO.ToEntity(),
                    sortingDTO.ToEntity(),
                    productFilterDTO.ToEntity())
               .Select(product => product.ToDTO())
               .ToList();
        }

        public ProductWithCategoryAndSizesDTO GetClientProduct(int productId)
        {
            Product? product = _productRepository.GetById(productId);

            if (product == null)
                throw new BusinessException("Product not found!", 404);


            return product.ToDTOWithCategoryAndSizes();
        }

        public async Task<ProductDTO> UploadProductImage(int id, string imageFile)
        {
            ProductDTO productDTO = GetSafeById(id);

            string folderName = GenerateProductContentFolder();

            string filePath = await UploadImage(imageFile, folderName);

            productDTO.ImageUrl = filePath;


            return await Update(productDTO);
        }

        #region[PRIVATE]
        private async Task<string> UploadImage(string imageFile, string folder)
        {
            PathRegistry pathRegistry = PathRegistry.GetInstance();

            string imagesFolder = Path.Combine(pathRegistry.WebRootPath, "images", "products", folder);

            var fileName =
                await FileHandler.Save(imageFile, Path.GetFullPath(imagesFolder), "image");



            return fileName;
        }

        private string GenerateProductContentFolder()
        {
            return Guid.NewGuid().ToString();
        }
        #endregion
    }
}
