using CrwnClothing.DAL.Entities;
using CrwnClothing.DAL.Models;
using CrwnClothing.DAL.Models.Sorting;

namespace CrwnClothing.DAL.Repositories.CategoryRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        public Category? GetCategoryByName(string name);
        public IEnumerable<Category> GetAllWithProducts(Pagination productsPagination,SortingModel sorting);
    }
}
