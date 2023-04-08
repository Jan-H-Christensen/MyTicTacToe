﻿using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.AspNetCore.SignalR.Client;
using MyTicTacToe.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeAPI.Enum;

namespace MyTicTacToe.Services
{
    partial class SignalR : ObservableObject
    {
        public bool IsConnected => hubConnection?.State == HubConnectionState.Connected;

        private string IP = "localhost";
        private string Port = "7011";

        public HubConnection? hubConnection;

        public static SignalR? _instance;

        private SignalR()
        {
        }
        public static SignalR GetInstance()
        {
            return _instance ??= new SignalR();
        }
        public ObservableCollection<TicTacToe> ticTacToeList { get; set; } = new ObservableCollection<TicTacToe>();

        [ObservableProperty]
        public bool _yourTurn;

        private SessionStart group;

        public SessionStart Group
        {
            get { return group; }
            set { group = value; }
        }

        [ObservableProperty]
        public Player _playerGroupe;

        [ObservableProperty]
        public SessionStart _session;

        [ObservableProperty]
        public string _player1 = "";
        [ObservableProperty]
        public string _player2 = "";


        public async Task StartGame(Player MyPlayer) 
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl("https://" + IP + ":" + Port + "/GameHub")
                .WithAutomaticReconnect()
                .Build();

            hubConnection.On<Player>("PlayerConnected",(Player) =>
            {
                Player2 = Player.Name;
                PlayerGroupe = Player;
            });

            hubConnection.On<SessionStart>("SessionStart",(SessionKey) =>
            {
                YourTurn = SessionKey.X_Or_O;
                this.Session = SessionKey;
            });

            hubConnection.On<SessionStart>("GameUpdate", (SessionKey) =>
            {
                foreach (TicTacToe item in ticTacToeList)
                {
                    if (item.Index == SessionKey.ticTacToe.Index)
                    {
                        item.GetSelectedText = SessionKey.ticTacToe.GetSelectedText;
                    }
                }
                YourTurn = SessionKey.X_Or_O;
                this.Session = SessionKey;
            });

            await hubConnection.StartAsync();

            await hubConnection.SendAsync("ConnectToGameSession", MyPlayer);
            Player1 = MyPlayer.Name;
        }

        public async Task StartSession(SessionStart sessionKey)
        {
            await hubConnection.SendAsync("StartGameSession", sessionKey);
        }

        public async Task UpdateSession(SessionStart sessionKey)
        {
            await hubConnection.SendAsync("UpdateGameSession", sessionKey);
        }
    }
}
