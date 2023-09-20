using CrwnClothing.DAL.Entities;
using CrwnClothing.DAL.Models;
using CrwnClothing.DAL.Models.Filtering;
using CrwnClothing.DAL.Models.Sorting;

namespace CrwnClothing.DAL.Repositories.ProductRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        public IEnumerable<Product> GetAll(Pagination pagination, ProductFilterModel filter);
        public IEnumerable<Product> GetAll(Pagination pagination, SortingModel sorting , ProductFilterModel filter);
        public IEnumerable<Product> GetAllByCategory(string categoryName,Pagination pagination, SortingModel sorting, ProductFilterModel filter);
    }
}
