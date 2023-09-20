
using CrwnClothing.BLL.Contracts.Enums;

namespace CrwnClothing.BLL.DTOs.OrderDTOs
{
    public class OrderDTO
    {
        public OrderDTO() 
        {
        }

        public OrderDTO(OrderDTO other) 
        {
            Id = other.Id;
            Subtotal = other.Subtotal;
            Total = other.Total;    
            OrderStatus = other.OrderStatus;
            PaymentStatus = other.PaymentStatus;
            ChargedTotal = other.ChargedTotal;
            PaymentIntentId = other.PaymentIntentId;
            ShippingAddress = other.ShippingAddress;
            ShippingCity = other.ShippingCity;
            ShippingCountry = other.ShippingCountry;
            ShippingZipCode = other.ShippingZipCode;
            CustomerId = other.CustomerId;
            CreatedAt = other.CreatedAt;
            UpdatedAt = other.UpdatedAt;
        }

        public int Id { get; set; } 
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
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
