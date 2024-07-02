using Microsoft.AspNetCore.SignalR;
using MobleFinalServer.Models;
using MySqlX.XDevAPI;
using System.Text.RegularExpressions;

namespace MobleFinalServer.Service
{
    public class SensorHub : Hub
    {
        public async Task SendMessage(string clientSerial, Sensor sensorData)
        {
            await Clients.Group(clientSerial).SendAsync("RT_SensorData", sensorData);
        }

        public async Task AddToGroup(string clientSerial)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, clientSerial);
        }

        public override async Task OnConnectedAsync()
        {
            string clientSerial = Context.GetHttpContext().Request.Query["clientSerial"];
            await AddToGroup(clientSerial);
            await base.OnConnectedAsync();
        }

        public async Task SensorDataToGroup(string clientSerial, Sensor sensor)
        {
            await Clients.Group(clientSerial).SendAsync("RT_SensorData", sensor);
        }
    }
}
