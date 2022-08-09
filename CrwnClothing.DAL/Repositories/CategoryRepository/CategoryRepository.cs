using CrwnClothing.DAL.Context;
using CrwnClothing.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrwnClothing.DAL.Repositories.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly CrwnClothingContext _context;

        public CategoryRepository(CrwnClothingContext context)
        {
            _context = context;
        }

        public Category CreateCategory(Category category)
        {
            _context.Categories.Add(category);

            _context.SaveChanges();


            return category;
        }

        public Category DeleteCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories;
        }

        public Category? GetWithProducts(int categoryId)
        {
            return _context.Categories
                .Include(c => c.Products)
                .Where(c => c.Id == categoryId)
                .FirstOrDefault();
        }

        public Category? GetCategory(int id)
        {
            return _context.Categories.Where(c => c.Id == id).FirstOrDefault();
        }

        public Category? GetCategoryByName(string name)
        {
            return _context.Categories.Where(c => c.Name == name).FirstOrDefault();
        }

        public Category UpdateCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public Category? GetWithProducts(string categoryName)
        {
            return _context.Categories
               .Include(c => c.Products)
               .Where(c => c.Name == categoryName)
               .FirstOrDefault();
        }

        public IEnumerable<Category> GetAllWithProducts()
        {
            return _context.Categories.Include(c => c.Products);
        }
    }
}
