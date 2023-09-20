using CrwnClothing.DAL.Context;
using CrwnClothing.DAL.Entities;
using CrwnClothing.DAL.Models;
using CrwnClothing.DAL.Helpers;
using CrwnClothing.DAL.Models.Filtering;
using CrwnClothing.DAL.Helpers.Filtering;
using CrwnClothing.DAL.Models.Sorting;
using Microsoft.EntityFrameworkCore;

namespace CrwnClothing.DAL.Repositories.ProductRepository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly CrwnClothingContext _context;

        public ProductRepository(CrwnClothingContext crwnClothingContext) : base(crwnClothingContext)
        {
            _context = crwnClothingContext;
        }

        public IEnumerable<Product> GetAll(Pagination pagination, ProductFilterModel filter)
        {

            return _context.Products.
                FilterProducts(filter)
                .Page(pagination);
        }

        public IEnumerable<Product> GetAll(Pagination pagination, SortingModel sorting, ProductFilterModel filter)
        {
            return _context.Products
                .FilterProducts(filter)
                .OrderByPropertyName(sorting.OrderBy, sorting.OrderDirection)
                .Page(pagination);
        }

        public IEnumerable<Product> GetAllByCategory(string categoryName, Pagination pagination, SortingModel sorting, ProductFilterModel filter)
        {
            return _context.Products
                .Include(p => p.Category)
                .Where(p => (p.Category != null && p.Category.Name == categoryName))
               .FilterProducts(filter)
               .OrderByPropertyName(sorting.OrderBy, sorting.OrderDirection)
               .Page(pagination);
        }
    }
}
