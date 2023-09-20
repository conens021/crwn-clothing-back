using Microsoft.AspNetCore.Mvc;

namespace CrwnClothing.Presentation.Models
{
    public class ProductFilterModel
    {
        [FromQuery(Name = "priceFrom")]
        public decimal? PriceFrom { get; set; }
        [FromQuery(Name = "priceTo")]
        public decimal? PriceTo { get; set; }
        [FromQuery(Name = "ColorIds[]")]
        public int?[]? ColorIds { get; set; }
    }
}
