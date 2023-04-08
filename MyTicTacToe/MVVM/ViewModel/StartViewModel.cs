using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyTicTacToe.MVVM.Model;

namespace MyTicTacToe.MVVM.ViewModel
{
 
    public partial class StartViewModel : ObservableObject
    {
        [ObservableProperty]
        public Player _playerOne;


        [ObservableProperty]
        public string _error;

        public StartViewModel()
        {
            PlayerOne = new Player();
            PlayerTwo = new Player();
        }

        [RelayCommand]
        public async Task GoToGameBoard()
        {
            if(PlayerOne.Name != null || PlayerTwo.Name != null) 
            {
                await Shell.Current.GoToAsync($"//TicTacToe",true, 
                    new Dictionary<string, object>
                    {
                        ["PlayerOne"] = PlayerOne,
                        ["PlayerTwo"] = PlayerTwo
                    });
                PlayerOne = new Player();
                PlayerTwo = new Player();
            }
            else
            {
                Error = "please enter a name for bothe player!!";
            }
        }
    }
}
