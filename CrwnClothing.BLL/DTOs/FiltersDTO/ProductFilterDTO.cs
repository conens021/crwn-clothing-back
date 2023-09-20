namespace CrwnClothing.BLL.DTOs.FiltersDTO
{
    public class ProductFilterDTO
    {
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        public int?[]? ColorIds { get; set; }
    }
}
