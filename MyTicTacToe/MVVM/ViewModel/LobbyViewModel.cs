using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyTicTacToe.MVVM.Model;
using MyTicTacToe.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            signalR.Session = new SessionStart{ GroupName = Players.GroupName, X_Or_O = true };
            await signalR.StartSession(new SessionStart { GroupName = Players.GroupName, X_Or_O = false });
            await Shell.Current.GoToAsync($"//TicTacToe");
            Players = new Player();
        }
    }
}
