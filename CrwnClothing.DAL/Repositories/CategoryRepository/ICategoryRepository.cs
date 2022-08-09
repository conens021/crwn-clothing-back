using CrwnClothing.DAL.Entities;

namespace CrwnClothing.DAL.Repositories.CategoryRepository
{
    public interface ICategoryRepository
    {
        public Category? GetCategory(int id);
        public Category? GetCategoryByName(string name);
        public Category CreateCategory(Category user);
        public Category UpdateCategory(Category user);
        public Category DeleteCategory(Category user);
        public IEnumerable<Category> GetAll();
        public Category? GetWithProducts(int id);
        public Category? GetWithProducts(string categoryName);
        public IEnumerable<Category> GetAllWithProducts();

    }
}
