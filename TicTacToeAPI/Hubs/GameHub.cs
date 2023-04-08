using Microsoft.AspNetCore.SignalR;
using MyTicTacToe.MVVM.Model;
using System.Numerics;
using TicTacToeAPI.Enum;

namespace TicTacToeAPI.Hubs
{
    public class GameHub : Hub
    {
        public async Task ConnectToGameSession(Player player) 
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, player.GroupName);

            await Clients.OthersInGroup(player.GroupName).SendAsync(MethodEndPoint.PlayerConnected, player);
        }

        public async Task StartGameSession(SessionStart session)
        {
            await Clients.OthersInGroup(session.GroupName).SendAsync(MethodEndPoint.SessionStart, session);
        }

        public async Task UpdateGameSession(SessionControler session)
        {
            await Clients.OthersInGroup(session.GroupName).SendAsync(MethodEndPoint.GameUpdate, session);
        }
    }
}
