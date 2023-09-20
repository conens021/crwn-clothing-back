using CrwnClothing.DAL.Repositories.CategoryRepository;
using CrwnClothing.BLL.Mappers;
using CrwnClothing.BLL.Mappers.FilteringMappers;
using CrwnClothing.DAL.Entities;
using DroneDropshipping.BLL.Exceptions;
using CrwnClothing.BLL.DTOs.CategoryDto;
using CrwnClothing.BLL.Helpers;
using CrwnClothing.BLL.DTOs;
using CrwnClothing.BLL.DTOs.SortingDto;

namespace CrwnClothing.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IProductService _productService;

        public CategoryService(
            ICategoryRepository repository,
            IProductService productService)
        {
            _repository = repository;
            _productService = productService;
        }

        #region[CRUD]
        public async Task<CategoryDTO> Create(CreateCategoryDTO categoryDTO)
        {
            if (!NameIsAvailable(categoryDTO.Name))
                throw new BusinessException("Category with given name already exists!", 409);


            Category forCreation = categoryDTO.ToEntity();

            if (categoryDTO.Image != null)
            {
                forCreation.CoverImageUrl = await UploadCoverImage(categoryDTO.Image);
            }

            Category created = await _repository.Create(forCreation);


            return created.ToDTO();
        }

        public Task<CategoryDTO> Delete(CategoryDTO categoryDTO)
        {
            throw new NotImplementedException();
        }


        public async Task<CategoryDTO> Update(CategoryDTO categoryDTO)
        {
            categoryDTO.UpdatedAt = DateTime.Now;

            Category updated = await _repository.Update(categoryDTO.ToEntity());


            return updated.ToDTO();
        }


        public CategoryDTO? GetById(int id)
        {
            Category? category = _repository.GetById(id);


            return category?.ToDTO();
        }

        public CategoryDTO GetSafeById(int id)
        {
            Category? category = _repository.GetById(id);

            if (category == null) throw new BusinessException("Category not found!", 404);

            return category.ToDTO();
        }
        #endregion

        public List<CategoryDTO> GetAll()
        {
            return _repository.GetAll().Select(c => c.ToDTO()).ToList();
        }

        public List<CategoryDTO> GetAllWithProducts(PaginationDTO paginationDTO, SortingDTO sortingDTO)
        {
            return _repository.GetAllWithProducts(paginationDTO.ToEntity(), sortingDTO.ToEntity())
                    .Select(c => c.ToDTOWithProducts())
                    .ToList();
        }

        public CategoryDTO GetCategoryByName(string name)
        {
            Category? category = _repository.GetCategoryByName(name);

            if (category == null) throw new BusinessException("Category does not exists!", 404);


            return category.ToDTO();
        }


        public async Task<CategoryDTO> UpdateCategoryImage(int id, string image)
        {
            CategoryDTO categoryDTO = this.GetSafeById(id);


            if (categoryDTO.CoverImageUrl != null)
            {
                this.DeleteCoverImage(categoryDTO.CoverImageUrl);
            }

            categoryDTO.CoverImageUrl = await UploadCoverImage(image);

            CategoryDTO updated = await this.Update(categoryDTO);


            return updated;
        }

        private async Task<string> UploadCoverImage(string image)
        {
            PathRegistry pathRegistry = PathRegistry.GetInstance();

            string folderPath =
                Path.Combine(pathRegistry.ImagesBasePath, pathRegistry.CategoriesPath);

            string fileName = await FileHandler.Save(image, folderPath, "image");


            return fileName;
        }

        private bool DeleteCoverImage(string fileName)
        {
            try
            {
                PathRegistry pathRegistry = PathRegistry.GetInstance();

                string folder = Path.Combine(pathRegistry.ImagesBasePath, pathRegistry.CategoriesPath);

                FileHandler.Delete(Path.Combine(folder, fileName));


                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool NameIsAvailable(string name)
        {
            Category? category = _repository.GetCategoryByName(name);


            return category == null;
        }

        public List<CategoryDTO> GetAll(PaginationDTO paginationDTO)
        {
            throw new NotImplementedException();
        }

        public List<CategoryDTO> GetAll(PaginationDTO paginationDTO, SortingDTO sorting)
        {
            throw new NotImplementedException();
        }

    }
}
