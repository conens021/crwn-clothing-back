using CrwnClothing.BLL.External.Contracts.Stripe;
using CrwnClothing.BLL.Services.External;
using CrwnClothing.DAL.Entities;
using CrwnClothing.Presentation.Attributes;
using CrwnClothing.Presentation.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CrwnClothing.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebHooksController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly StripeAuthorizationManager _stripeAuth;
        private readonly IConfiguration _configuration;
        private readonly string _endpointSecret;
        private readonly IHubContext<ProductInventoryHub> _hubContext;

        public WebHooksController(
            IPaymentService paymentService,
            StripeAuthorizationManager stripeAuth,
            IConfiguration config,
            IHubContext<ProductInventoryHub> hubContext)
        {
            _paymentService = paymentService;
            _stripeAuth = stripeAuth;
            _configuration = config;
            _endpointSecret = _configuration["Stripe:WebHookSecret"];
            _hubContext = hubContext;
        }

        [HttpPost("stripe")]
        public async Task<IActionResult> HandleStripeEvent()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            await _paymentService.HandleEvent(json, _stripeAuth.GetSignature(), _endpointSecret);


            return Ok();
        }

        [HttpPost("test-hub")]
        public async Task<IActionResult> TestHub(int productId)
        {
            await _hubContext.Clients.Group(productId.ToString())
                .SendAsync("ProductOutOfStock");


            return Ok();
        }
    }
}
