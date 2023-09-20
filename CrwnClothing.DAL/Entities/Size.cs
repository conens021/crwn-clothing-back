namespace CrwnClothing.DAL.Entities
{
    public partial class Size : BaseEntity
    {
        public Size()
        {
            ProductsSizes = new HashSet<ProductsSize>();
            OrderProducts = new HashSet<OrderProduct>();
            ShoppingCartProducts = new HashSet<ShoppingCartProduct>();
        }

        public string Name { get; set; } = null!;
        public byte Value { get; set; }

        public virtual ICollection<ShoppingCartProduct> ShoppingCartProducts { get; set; }
        public virtual ICollection<ProductsSize> ProductsSizes { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
