using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyTicTacToe.MVVM.Model;
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
            await Shell.Current.GoToAsync($"//TicTacToe", true,
                new Dictionary<string, object>
                {
                    ["Players"] = Players,
                });
            Players = new Player();
        }
    }
}
