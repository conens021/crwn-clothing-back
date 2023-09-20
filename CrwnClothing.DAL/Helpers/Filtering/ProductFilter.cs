
using CrwnClothing.DAL.Entities;
using CrwnClothing.DAL.Models.Filtering;

namespace CrwnClothing.DAL.Helpers.Filtering
{
    public static class ProductFilter
    {
        public static IQueryable<Product> FilterProducts(this IQueryable<Product> products, ProductFilterModel filterModel)
        {

            return products
                .WhereIf(filterModel.PriceFrom != null, product => product.Price >= filterModel.PriceFrom)
                .WhereIf(filterModel.PriceTo != null, product => product.Price <= filterModel.PriceTo)
                .WhereIf(filterModel.ColorIds != null, product => 
#pragma warning disable CS8604 // Possible null reference argument.
                    filterModel.ColorIds.ToList().Contains(product.ColorId));
#pragma warning restore CS8604 // Possible null reference argument.
        }
    }
}
