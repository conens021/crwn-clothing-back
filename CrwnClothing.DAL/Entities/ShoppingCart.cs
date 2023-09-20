namespace CrwnClothing.DAL.Entities
{
    public partial class ShoppingCart : BaseEntity
    {
        public ShoppingCart()
        {
            ShoppingCartProducts = new HashSet<ShoppingCartProduct>();
        }

        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;

        public virtual ICollection<ShoppingCartProduct> ShoppingCartProducts { get; set; }
    }
}
