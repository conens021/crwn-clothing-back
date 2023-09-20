using CrwnClothing.BLL.DTOs.Custom;
using CrwnClothing.BLL.DTOs.OrderDTOs;
using CrwnClothing.BLL.Services.OrderService;
using CrwnClothing.Presentation.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace CrwnClothing.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly JwtAuthenticationManager _jwt;

        public OrdersController(IOrderService orderService, JwtAuthenticationManager jwt)
        {
            _orderService = orderService;
            _jwt = jwt;
        }

        [HttpPost("create-order-intent")]
        public async Task<IActionResult> CreateFromUser()
        {
            OrderIntentWithCartProductsDTO orderIntentDTO =
                await _orderService.CreateOrderIntent(_jwt.GetBearerUser());


            return Ok(orderIntentDTO);
        }

        [HttpPut("update-order-intent/{orderIntentId}")]
        public async Task<IActionResult> UpdateOrderIntent(int orderIntentId)
        {
            OrderIntentWithCartProductsDTO orderIntentDTO =
                await _orderService.UpdateOrderIntent(_jwt.GetBearerUser(), orderIntentId);


            return Ok(orderIntentDTO);
        }

        [HttpPatch("shipping-details/{id}")]
        public async Task<IActionResult> UpdateShippingDetails(
            int id, [FromBody] OrderShippingDetailsDTO shippingDetailsDTO)
        {
            OrderDTO orderDTO = await _orderService.Update(id, shippingDetailsDTO);


            return Ok(orderDTO);
        }

        [HttpPatch("start-order-request/{id}")]
        public async Task<IActionResult> UpdateOrderRequest(int id)
        {
            OrderDTO orderDTO = await _orderService.StartOrderRequest(id,_jwt.GetBearerUser());


            return Ok(orderDTO);
        }

        [HttpPatch("order-request-failed/{orderId}")]
        public async Task<IActionResult> OrderRequestFailed(int orderId)
        {
            OrderDTO orderDTO = await _orderService.OrderRequestFailed(orderId, _jwt.GetBearerUser());


            return Ok(orderDTO);
        }
    }
}
