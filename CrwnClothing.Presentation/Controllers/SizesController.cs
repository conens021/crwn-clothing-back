using CrwnClothing.BLL.DTOs.Custom.Cart;
using CrwnClothing.BLL.DTOs.SizesDTOs;
using CrwnClothing.BLL.Services.Sizes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrwnClothing.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizesController : ControllerBase
    {
        private readonly ISizeService _sizeService;

        public SizesController(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }

        [HttpGet("available/{productId}")]
        public IActionResult GetAvailableSizesByProductId(int productId)
        {
            return Ok(_sizeService.GetAvailableSizesByProduct(productId));
        }

        [HttpGet("check-size-quantity")]
        public IActionResult CheckSizeQuantity(AddToCartProductDTO cartItem)
        {
           SizeWithQuantityDTO sizeWithQuantityDTO =
                _sizeService.CheckProductQuantityAvailabilty(cartItem);

            return Ok(sizeWithQuantityDTO);
        }
    }
}
