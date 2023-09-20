using CrwnClothing.BLL.DTOs.FiltersDTO;
using CrwnClothing.DAL.Models.Filtering;

namespace CrwnClothing.BLL.Mappers.FilteringMappers
{
    public static class ProductFilterMapper
    {
        public static ProductFilterModel ToEntity(this ProductFilterDTO dto) => new()
        {
            PriceFrom = dto.PriceFrom,
            PriceTo = dto.PriceTo,
            ColorIds = dto.ColorIds
        };

    }
}
