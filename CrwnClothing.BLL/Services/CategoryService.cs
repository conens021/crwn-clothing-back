using CrwnClothing.BLL.DTOs;
using CrwnClothing.DAL.Repositories.CategoryRepository;
using CrwnClothing.BLL.Mappers;
using CrwnClothing.DAL.Entities;
using DroneDropshipping.BLL.Exceptions;

namespace CrwnClothing.BLL.Services
{
    public class CategoryService : ICategoryService
    {

        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public CategoryDTO CreateCategory(CategoryDTO categoryDTO)
        {
            try
            {
                this.GetCategoryByName(categoryDTO.Name);
            }
            catch (BusinessException)
            {
                categoryDTO.CreatedAt = DateTime.Now;
                categoryDTO.UpdatedAt = DateTime.Now;

                Category created = _repository.CreateCategory(categoryDTO.ToEntity());

                return created.ToDTO();
            }


            throw new BusinessException("Category with given name already exists!", 409);
        }

        public CategoryDTO DeleteCategory(CategoryDTO categoryDTO)
        {
            throw new NotImplementedException();
        }

        public List<CategoryDTO> GetAll()
        {
            return _repository.GetAll().Select(c => c.ToDTO()).ToList();
        }

        public List<CategoryDTO> GetAllWithProducts()
        {
            return _repository.GetAllWithProducts().Select(c => c.ToDTOWithProducts()).ToList();
        }

        public CategoryDTO GetCategory(int id)
        {
            throw new NotImplementedException();
        }

        public CategoryDTO GetCategoryByName(string name)
        {
            Category? category = _repository.GetCategoryByName(name);

            if (category == null) throw new BusinessException("Category does not exists!", 404);


            return category.ToDTO();
        }

        public CategoryDTO GetCategoryWithProducts(int id)
        {
            Category? category = _repository.GetWithProducts(id);

            if (category == null) throw new BusinessException("Category does not exists!", 404);


            return category.ToDTOWithProducts();
        }

        public CategoryDTO GetCategoryWithProducts(string name)
        {
            Category? category = _repository.GetWithProducts(name);

            if (category == null) throw new BusinessException("Category with given name does not exists!", 404);


            return category.ToDTOWithProducts();
        }

        public CategoryDTO UpdateCategory(CategoryDTO categoryDTO)
        {
            throw new NotImplementedException();
        }
    }
}
