using Microsoft.AspNetCore.SignalR;

namespace allSpice.Hubs;
public class UpdatesHub : Hub
{
 public async Task SendUpdate()
 {
  await Clients.All.SendAsync("receiveUpdate");
 }
}