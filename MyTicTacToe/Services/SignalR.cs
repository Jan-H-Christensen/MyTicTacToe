using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTicTacToe.Services
{
    internal class SignalR
    {
        string IP = "192.168.2.104";
        string Port = "5007";

        public async Task<HubConnection> ConnectBordHub() 
        {
            return new HubConnectionBuilder()
                .WithUrl("http://" + IP + Port + "/GameHub")
                .WithAutomaticReconnect()
                .Build();
        }

    }
}
