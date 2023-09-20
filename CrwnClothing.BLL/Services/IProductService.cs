
using CrwnClothing.BLL.DTOs;
using CrwnClothing.BLL.DTOs.FiltersDTO;
using CrwnClothing.BLL.DTOs.ProductDto;
using CrwnClothing.BLL.DTOs.SortingDto;

namespace CrwnClothing.BLL.Services
{
    public interface IProductService : IBaseService<ProductDTO, CreateProductDTO>
    {
        public List<ProductDTO> GetByIdBulk(List<int> ids);
        public List<ProductDTO> GetAll(PaginationDTO paginationDTO, ProductFilterDTO productFilterDTO);
        public List<ProductDTO> GetAll(PaginationDTO paginationDTO, SortingDTO sorting, ProductFilterDTO productFilterDTO);
        public List<ProductDTO> GetAllByCategory(
            string categoryName, PaginationDTO paginationDTO, SortingDTO sorting, ProductFilterDTO productFilterDTO);
        public ProductWithCategoryAndSizesDTO GetClientProduct(int productId);
    }
}
