using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyTicTacToe.MVVM.Model;

namespace MyTicTacToe.MVVM.ViewModel
{
 
    public partial class StartViewModel : ObservableObject
    {
        [ObservableProperty]
        public Player _players;

        [ObservableProperty]
        public string _error;

        public StartViewModel()
        {
            Players = new Player();
        }

        [RelayCommand]
        public async Task CreateGame()
        {
            if(Players.Name != null || Players.GroupName != null) 
            {
                await Shell.Current.GoToAsync($"//Lobby",true, 
                    new Dictionary<string, object>
                    {
                        ["Players"] = Players,
                    });
                Players = new Player();
            }
            else
            {
                Error = "please enter a name for bothe player!!";
            }
        }

        [RelayCommand]
        public async Task JoinGame()
        {
            if (Players.Name != null || Players.GroupName != null)
            {
                await Shell.Current.GoToAsync($"//Lobby", true,
                    new Dictionary<string, object>
                    {
                        ["Players"] = Players,
                    });
                Players = new Player();
            }
            else
            {
                Error = "please enter a name for bothe player!!";
            }
        }
    }
}
