using CrwnClothing.BLL.DTOs.Custom.Cart;
using CrwnClothing.BLL.DTOs.OrderDTOs;
using CrwnClothing.DAL.Entities;

namespace CrwnClothing.BLL.Mappers.OrderMappers
{
    public static class OrderProductMapper
    {

        public static OrderProduct ToEntity(this CreateOrderProductDTO dto) => new() 
        {
            OrderId = dto.OrderId,
            ProductId = dto.ProductId,
            Quantity = dto.Quantity,
            SizeId = dto.SizeId
        };

        public static CreateOrderProductDTO ToCreateOrderProductDTO(this CartProductDTO cartProduct, int orderId) => new()
        {
            OrderId = orderId,
            ProductId = cartProduct.Product.Id,
            Quantity = cartProduct.Quantity,
            SizeId = cartProduct.Size.Id
        };



        public static List<CreateOrderProductDTO> ToCreateOrderProductDTO(this List<CartProductDTO> cartProducts, int orderId)
        {
            return cartProducts.Select(cartProduct =>
                    cartProduct.ToCreateOrderProductDTO(orderId)).ToList();
        }

        public static OrderProductDTO ToDTO(this OrderProduct orderProduct) => new() 
        {
            Id = orderProduct.Id,
            OrderId = orderProduct.OrderId,
            ProductId = orderProduct.ProductId,
            Quantity =  orderProduct.Quantity,
            CreatedAt =orderProduct.CreatedAt,
            UpdatedAt = orderProduct.UpdatedAt,
        };

        public static OrderProduct ToEntity(this OrderProductDTO orderProductDTO) => new() 
        {
            Id = orderProductDTO.Id,
            OrderId = orderProductDTO.OrderId,
            ProductId = orderProductDTO.ProductId,
            Quantity = orderProductDTO.Quantity,
            CreatedAt = orderProductDTO.CreatedAt,
            UpdatedAt = orderProductDTO.UpdatedAt,
        };
    }
}
