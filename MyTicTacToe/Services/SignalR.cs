using CommunityToolkit.Mvvm.ComponentModel;
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
        public SessionStart _sessionStart;

        [ObservableProperty]
        public SessionControler _sessionControll;

        [ObservableProperty]
        public string _playerContainer;

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

            hubConnection.On<Player>(MethodEndPoint.PlayerConnected,(Player) =>
            {
                PlayerContainer = Player.Name;
            });

            hubConnection.On<SessionStart>(MethodEndPoint.SessionStart,(SessionKey) =>
            {
                SessionStart = SessionKey;
                Player1 = SessionKey.Player1;
                Player2 = SessionKey.Player2;
            });

            hubConnection.On<SessionControler>(MethodEndPoint.GameUpdate, (SessionKey) =>
            {
                foreach (TicTacToe item in ticTacToeList)
                {
                    if (item.Index == SessionKey.ticTacToe.Index)
                    {
                        item.GetSelectedText = SessionKey.ticTacToe.GetSelectedText;
                    }
                }
                YourTurn = SessionKey.Turn;
            });

            await hubConnection.StartAsync();

            await hubConnection.SendAsync(MethodEndPoint.ConnectToGameSession, MyPlayer);
        }

        public async Task StartSession(SessionStart sessionKey)
        {
            await hubConnection.SendAsync(MethodEndPoint.StartGameSession, sessionKey);
        }

        public async Task UpdateSession(SessionControler sessionKey)
        {
            await hubConnection.SendAsync(MethodEndPoint.UpdateGameSession, sessionKey);
        }
    }
}
