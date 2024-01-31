using Microsoft.AspNetCore.SignalR;

namespace EldenGuide.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        { 
            Console.WriteLine("jeremy is fat");
            Console.WriteLine("jeremy is wrong" + message);
            Clients.All.SendAsync("ReceiveMessage", user, message);

        }
    }
}
