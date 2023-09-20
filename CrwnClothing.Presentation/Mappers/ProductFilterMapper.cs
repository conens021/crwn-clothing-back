using CrwnClothing.BLL.DTOs.FiltersDTO;
using CrwnClothing.Presentation.Models;

namespace CrwnClothing.Presentation.Mappers
{
    public static class ProductFilterMapper
    {
        public static ProductFilterDTO ToDTO(this ProductFilterModel model) => new()
        {
            PriceFrom = model.PriceFrom,
            PriceTo = model.PriceTo,
            ColorIds = model.ColorIds
        };
    }
}
