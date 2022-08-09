
using CrwnClothing.BLL.DTOs;

namespace CrwnClothing.BLL.Services
{
    public interface ICategoryService
    {
        public CategoryDTO GetCategory(int id);
        public CategoryDTO GetCategoryWithProducts(int id);
        public CategoryDTO GetCategoryByName(string name);
        public CategoryDTO GetCategoryWithProducts(string name);
        public CategoryDTO CreateCategory(CategoryDTO user);
        public CategoryDTO UpdateCategory(CategoryDTO user);
        public CategoryDTO DeleteCategory(CategoryDTO user);
        public List<CategoryDTO> GetAll();
        public List<CategoryDTO> GetAllWithProducts();
    }
}
