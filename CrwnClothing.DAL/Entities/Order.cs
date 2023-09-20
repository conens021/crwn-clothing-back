namespace CrwnClothing.DAL.Entities
{
    public partial class Order : BaseEntity
    {
        public Order()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public int OrderStatus { get; set; }
        public int PaymentStatus { get; set; }
        public decimal ChargedTotal { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ShippingAddress { get; set; }
        public string? ShippingCity { get; set; }
        public string? ShippingCountry { get; set; }
        public string? ShippingZipCode { get; set; }
        public int CustomerId { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
