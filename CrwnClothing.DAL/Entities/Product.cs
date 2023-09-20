namespace CrwnClothing.DAL.Entities
{
    public partial class Product : BaseEntity
    {
        public Product()
        {
            OrderProducts = new HashSet<OrderProduct>();
            ProductsSizes = new HashSet<ProductsSize>();
            ShoppingCartProducts = new HashSet<ShoppingCartProduct>();
        }

        public string Name { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int? CategoryId { get; set; }
        public int? ColorId { get; set; }
        public int? ColorCollectionId { get; set; }
        public string? ContentId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ColorCollection? ColorCollection { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
        public virtual ICollection<ProductsSize> ProductsSizes { get; set; }
        public virtual ICollection<ShoppingCartProduct> ShoppingCartProducts { get; set; }
    }
}
