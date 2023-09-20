using CrwnClothing.BLL.Contracts.Enums;

namespace CrwnClothing.BLL.DTOs.OrderDTOs
{
    public class CreateOrderDTO
    {
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public decimal ChargedTotal { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ShippingAddress { get; set; }
        public string? ShippingCity { get; set; }
        public string? ShippingCountry { get; set; }
        public string? ShippingZipCode { get; set; }
        public int CustomerId { get; set; }
    }
}
