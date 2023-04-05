using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyTicTacToe.MVVM.Model;
using MyTicTacToe.MVVM.View;
using MyTicTacToe.Services;
using System.Collections.ObjectModel;

namespace MyTicTacToe.MVVM.ViewModel
{
    [QueryProperty(nameof(PlayerList), nameof(PlayerList))]
    public partial class HeighscoreViewModel : ObservableObject
    {
        //private ItemDatabase database = null;

        [ObservableProperty]
        public ObservableCollection<Player> _playerList;

        public HeighscoreViewModel()
        {
            //database = ItemDatabase;
            PlayerList = new ObservableCollection<Player>();
        }


        
        [RelayCommand]

        public async Task GoToMain()
        {
            await Shell.Current.GoToAsync($"//Start",true);
            PlayerList = new ObservableCollection<Player>();
        }
    }
}
