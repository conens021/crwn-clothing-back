namespace CrwnClothing.DAL.Models.Filtering
{
    public class ProductFilterModel : BaseFilterModel
    {
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set;}
        public int?[]? ColorIds { get; set; }
    }
}
