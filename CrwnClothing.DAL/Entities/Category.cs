namespace CrwnClothing.DAL.Entities
{
    public partial class Category : BaseEntity
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public string Name { get; set; } = null!;
        public string? CoverImageUrl { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
