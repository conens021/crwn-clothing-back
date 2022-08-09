using Microsoft.AspNetCore.SignalR;

namespace CrwnClothing.Presentation.Hubs
{
    public class ChatHub : Hub
    {

        private readonly string _botUser;
        private readonly IDictionary<string, UserConnection> _connections;

        public ChatHub(IDictionary<string, UserConnection> connections)
        {
            this._botUser = "Chat Bot";
            this._connections = connections;
        }


        public override async Task<Task> OnDisconnectedAsync(Exception? exception)
        {

            if (_connections.TryGetValue(Context.ConnectionId, out UserConnection? userConnection))
            {
                _connections.Remove(Context.ConnectionId);

                await Clients.Group(userConnection.Room)
                   .SendAsync("RecieveMessage", _botUser, $"{userConnection.UserName} has leave the room");

                await SendConnectedUsers(userConnection.Room);
            }

            return base.OnDisconnectedAsync(exception);
        }

        //for sending messages
        public async Task SendMessage(string message)
        {
            if (_connections.TryGetValue(Context.ConnectionId, out UserConnection? userConnection))
            {
                await Clients.Group(userConnection.Room)
                    .SendAsync("RecieveMessage", userConnection.UserName, message);
                
            }
        }


        //join room method
        //this method is called when ever user whant to join the room
        //user need to send username and room he want to join
        public async Task JoinRoom(UserConnection userConnection)
        {

            await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.Room);

            _connections.Add(Context.ConnectionId, userConnection);

            await Clients.Group(userConnection.Room)
                .SendAsync("RecieveMessage", _botUser, $"{userConnection.UserName} has joined {userConnection.Room}");

            await SendConnectedUsers(userConnection.Room);
        }

        private async Task SendConnectedUsers(string room)
        {
            IEnumerable<string> users =
                _connections.Values
                .Where(uc => uc.Room == room)
                .Select(uc => uc.UserName);

            await Clients.Group(room).SendAsync("ConnectedUsers", users);
        }

    }
}
