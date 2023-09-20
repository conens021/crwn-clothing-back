namespace CrwnClothing.DAL.Entities
{
    public partial class ProductsSize : BaseEntity
    {
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public int QuantityAvailable { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual Size Size { get; set; } = null!;
    }
}
