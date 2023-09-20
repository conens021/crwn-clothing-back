using CrwnClothing.DAL.Context;
using CrwnClothing.DAL.Entities;
using CrwnClothing.DAL.Models;
using CrwnClothing.DAL.Models.Sorting;
using Microsoft.EntityFrameworkCore;
using CrwnClothing.DAL.Helpers;
using Z.EntityFramework.Plus;

namespace CrwnClothing.DAL.Repositories.CategoryRepository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {

        private readonly CrwnClothingContext _context;

        public CategoryRepository(CrwnClothingContext context) : base(context)
        {
            _context = context;
        }

        public Category? GetWithProducts(int categoryId)
        {
            return _context.Categories
                .Include(c => c.Products)
                .Where(c => c.Id == categoryId)
                .FirstOrDefault();
        }

        public Category? GetCategoryByName(string name)
        {
            return _context.Categories.Where(c => c.Name == name).FirstOrDefault();
        }

        public Category? GetWithProducts(string categoryName)
        {
            return _context.Categories
               .Include(c => c.Products)
               .Where(c => c.Name == categoryName)
               .FirstOrDefault();
        }

        public IEnumerable<Category> GetAllWithProducts(Pagination pagination, SortingModel sorting)
        {

            return _context.Categories
                .OrderByPropertyName(sorting.OrderBy, sorting.OrderDirection).
                Page(pagination)
                .IncludeFilter(c =>
                    c.Products.OrderByDescending(p => p.CreatedAt)
                    .Take(4));
        }

    }
}
