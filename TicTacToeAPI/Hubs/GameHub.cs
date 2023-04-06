using Microsoft.AspNetCore.SignalR;
using MyTicTacToe.MVVM.Model;
using TicTacToeAPI.Enum;

namespace TicTacToeAPI.Hubs
{
    public class GameHub : Hub
    {
        public Task SendGame(TicTacToe ticTacToe) 
        {
            Console.Out.WriteLineAsync(ticTacToe.Index+" : "+ticTacToe.getSelectedText);
            return Clients.All.SendAsync(MethodEndPoint.SendBord,ticTacToe);
        }
    }
}
