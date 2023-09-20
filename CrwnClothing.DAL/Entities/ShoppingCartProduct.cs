namespace CrwnClothing.DAL.Entities
{
    public partial class ShoppingCartProduct : BaseEntity
    {
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int ShoppingCartId { get; set; }
        public int SizeId { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual ShoppingCart ShoppingCart { get; set; } = null!;
        public virtual Size Size { get; set; } = null!;
    }
}
