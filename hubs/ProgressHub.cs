using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalR.Hubs 
{
    public class ProgressHub : Hub
    {
        public async Task SendProgressUpdate(int percentage)
        {
            await Clients.All.SendAsync("ReceiveProgressUpdate", percentage);
        }
    }
}
