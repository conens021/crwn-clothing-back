namespace CrwnClothing.DAL.Entities
{
    public partial class ColorCollection : BaseEntity
    {
        public ColorCollection()
        {
            Products = new HashSet<Product>();
        }

        public string Name { get; set; } = null!;
        public string? PhotoUrl { get; set; }
        public string? ContentId { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
