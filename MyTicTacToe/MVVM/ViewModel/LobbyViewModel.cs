using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyTicTacToe.MVVM.Model;
using MyTicTacToe.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeAPI.Enum;

namespace MyTicTacToe.MVVM.ViewModel
{
    [QueryProperty(nameof(Players),nameof(Players))]
    [QueryProperty(nameof(IsVisable), nameof(IsVisable))]
    public partial class LobbyViewModel : ObservableObject
    {
        private SignalR signalR = SignalR.GetInstance();

        [ObservableProperty]
        public Player _players;

        [ObservableProperty]
        public bool _isVisable;
        public LobbyViewModel()
        {
            Players = new Player();
            IsVisable = false;
        }

        [RelayCommand]
        public async Task GoToGame()
        {
            signalR.SessionStart = new SessionStart{ GroupName = Players.GroupName, SessionChar = MethodEndPoint.X, Player1 = Players.Name,Player2 = signalR.PlayerContainer };
            await signalR.StartSession(new SessionStart { GroupName = Players.GroupName, SessionChar = MethodEndPoint.O, Player1 = Players.Name, Player2 = signalR.PlayerContainer });
            await Shell.Current.GoToAsync($"//TicTacToe");
            Players = new Player();
        }
    }
}
