using Microsoft.AspNetCore.SignalR;

namespace CrwnClothing.Presentation.Hubs
{
    public class ProductInventoryHub : Hub
    {
        private readonly ILogger _logger;


        public ProductInventoryHub(ILogger logger)
        {
            _logger = logger;
        }   


        public override async Task OnConnectedAsync()
        {
            _logger.LogInformation($"{Context.ConnectionId} Connected!");

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? ex)
        {
            _logger.LogInformation($"{Context.ConnectionId} Disconnected!");
            
            await base.OnDisconnectedAsync(ex);
        }

        public async Task JoinProductInventoryRoom(string productId)
        {
            _logger.LogInformation($"{Context.ConnectionId} Joined to {productId} room");

            await Groups.AddToGroupAsync(Context.ConnectionId, productId);
        }

        public async Task SendProductQuantityCritical(int productId, int quantity)
        {
            await Clients.Group(productId.ToString())
                    .SendAsync("ProductQuantityCritical", quantity);

        }
    }
}
