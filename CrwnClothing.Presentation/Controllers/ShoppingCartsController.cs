using CrwnClothing.BLL.DTOs.Custom.Cart;
using CrwnClothing.BLL.DTOs.ShoppingCartDto;
using CrwnClothing.BLL.Services.ShppingCart;
using CrwnClothing.Presentation.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrwnClothing.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShoppingCartsController : ControllerBase
    {
        private readonly IShoppingCartService _service;
        private readonly JwtAuthenticationManager _jwt;

        public ShoppingCartsController(IShoppingCartService service, JwtAuthenticationManager jwt)
        {
            _service = service;
            _jwt = jwt;
        }

        [HttpGet]
        public async Task<IActionResult> GetCartWithProducts()
        {
            ShoppingCartWithProductsDTO sc =
                await _service.GetUserCartWithCartProducts(_jwt.GetBearerUser());


            return Ok(sc);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartProductDTO cartProductDTO)
        {
            List<CartProductDTO> cartProducts =
                 await _service.AddToCart(cartProductDTO, _jwt.GetBearerUser());

            return Ok(cartProducts);
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> AddToCartBulk([FromBody] List<AddToCartProductDTO> cartProductDTO)
        {
            List<CartProductDTO> cartProducts =
                 await _service.AddToCart(cartProductDTO, _jwt.GetBearerUser());

            return Ok(cartProducts);
        }

        [HttpPatch("quantity")]
        public async Task<IActionResult> ChangeQuantity(AddToCartProductDTO productCartDTO)
        {
            List<CartProductDTO> cartProducts =
                 await _service.ChangeQuantity(productCartDTO, _jwt.GetBearerUser());

            return Ok(cartProducts);
        }

        [HttpDelete("")]
        public async Task<IActionResult> RemoveFromCart([FromQuery] DeleteShoppingCartItemDTO deleteDTO)
        {
            List<CartProductDTO> cartProducts =
                 await _service.RemoveFromCart(deleteDTO, _jwt.GetBearerUser());

            return Ok(cartProducts);
        }

        [HttpDelete("empty-cart")]
        public IActionResult EmptyUserCart()
        {
            List<CartProductDTO> cartProductDTOs = _service.EmptyUserCart(_jwt.GetBearerUser());


            return Ok(cartProductDTOs);
        }
    }
}
