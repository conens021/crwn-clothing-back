using CrwnClothing.BLL.Contracts.Enums;
using CrwnClothing.BLL.DTOs.Custom;
using CrwnClothing.BLL.DTOs.Custom.Cart;
using CrwnClothing.BLL.DTOs.Custom.Payments;
using CrwnClothing.BLL.DTOs.OrderDTOs;
using CrwnClothing.DAL.Entities;

namespace CrwnClothing.BLL.Mappers.OrderMappers
{
    public static class OrderMapper
    {
        public static Order ToEntity(this CreateOrderDTO createorderDTO) => new()
        {
            ChargedTotal = 0.00m,
            CreatedAt = DateTime.Now,
            CustomerId = createorderDTO.CustomerId,
            OrderStatus = (int)createorderDTO.OrderStatus,
            PaymentStatus = (int)createorderDTO.PaymentStatus,
            Subtotal = createorderDTO.Subtotal,
            Total = createorderDTO.Total,
            PaymentIntentId = createorderDTO.PaymentIntentId
        };

        public static Order ToEntity(this OrderDTO orderDTO) => new()
        {
            Id = orderDTO.Id,
            Subtotal = orderDTO.Subtotal,
            Total = orderDTO.Total,
            ChargedTotal = orderDTO.ChargedTotal,
            OrderStatus = (int)orderDTO.OrderStatus,
            PaymentStatus = (int)orderDTO.PaymentStatus,
            PaymentIntentId = orderDTO.PaymentIntentId,
            ShippingAddress = orderDTO.ShippingAddress,
            ShippingCity = orderDTO.ShippingCity,
            ShippingCountry = orderDTO.ShippingCountry,
            ShippingZipCode = orderDTO.ShippingZipCode,
            CreatedAt = orderDTO.CreatedAt,
            UpdatedAt = orderDTO.UpdatedAt,
            CustomerId = orderDTO.CustomerId,
        };

        public static OrderDTO ToDTO(this Order entity) => new()
        {
            Id = entity.Id,
            Subtotal = entity.Subtotal,
            Total = entity.Total,
            ChargedTotal = entity.ChargedTotal,
            CustomerId = entity.CustomerId,
            OrderStatus = (OrderStatus)entity.OrderStatus,
            PaymentStatus = (PaymentStatus)entity.PaymentStatus,
            PaymentIntentId = entity.PaymentIntentId,
            ShippingAddress = entity.ShippingAddress,
            ShippingCountry = entity.ShippingCountry,
            ShippingCity = entity.ShippingCity,
            ShippingZipCode = entity.ShippingZipCode,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };

        public static OrderDTO ToDTO(this OrderShippingDetailsDTO shippingDTO, OrderDTO updatingDTO)
        {
            updatingDTO.ShippingAddress = shippingDTO.ShippingAddress;
            updatingDTO.ShippingCountry = shippingDTO.ShippingCountry;
            updatingDTO.ShippingCity = shippingDTO.ShippingCity;
            updatingDTO.ShippingZipCode = shippingDTO.ShippingZipCode;


            return updatingDTO;
        }

        public static OrderWithProductsDTO ToOrderWithProductsDTO(
            this OrderDTO dto,
            List<CartProductDTO> products) => new(dto, products){};


        public static OrderIntentDTO ToOrderIntentDTO(
            this OrderDTO orderDTO, CcPaymenIntetntDTO paymentDTO) => new(orderDTO, paymentDTO) { };


        public static OrderIntentWithCartProductsDTO ToOrderIntentWithCartProductsDTO(
            this OrderDTO orderDTO, CcPaymenIntetntDTO paymentDTO, List<CartProductDTO> cartProduts) =>
            new(orderDTO, paymentDTO, cartProduts) { };

    }
}
