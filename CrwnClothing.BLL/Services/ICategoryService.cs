
using CrwnClothing.BLL.DTOs;
using CrwnClothing.BLL.DTOs.CategoryDto;
using CrwnClothing.BLL.DTOs.SortingDto;

namespace CrwnClothing.BLL.Services
{
    public interface ICategoryService : IBaseService<CategoryDTO,CreateCategoryDTO>
    {
        public CategoryDTO GetCategoryByName(string name);
        public List<CategoryDTO> GetAllWithProducts(PaginationDTO paginationDTO,SortingDTO sortingDto);
        public Task<CategoryDTO> UpdateCategoryImage(int id, string image);
    }
}
