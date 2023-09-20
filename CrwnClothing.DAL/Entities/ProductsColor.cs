namespace CrwnClothing.DAL.Entities
{
    public partial class ProductsColor : BaseEntity
    {
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public string? ContentId { get; set; }
        public string? ImageUrl { get; set; }
    }
}
