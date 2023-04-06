using Android.Graphics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.SignalR.Client;
using MyTicTacToe.MVVM.Model;
using MyTicTacToe.Services;
using System.Collections.ObjectModel;
using TicTacToeAPI.Enum;

namespace MyTicTacToe.MVVM.ViewModel
{
    [QueryProperty(nameof(PlayerOne), nameof(PlayerOne))]
    [QueryProperty(nameof(PlayerTwo), nameof(PlayerTwo))]
    public partial class TicTacToeViewModel : ObservableObject
    {
        ItemDatabase database = null;

        private HubConnection? hubConnection;

        [ObservableProperty]
        private Player _playerOne;

        [ObservableProperty]
        private Player _playerTwo;

        [ObservableProperty]
        private string _winOrDrawText;

        private int _playerTurn = 0;
        private bool _isAnyoneWin;

        List<int[]> winPositions = new List<int[]>();
        public ObservableCollection<TicTacToe> ticTacToeList { get; set; } = new ObservableCollection<TicTacToe>();

        [ObservableProperty]
        public ObservableCollection<Player> _playerList;
        //public Player Player { get; set; }
        public TicTacToeViewModel()
        {
            SetupGameBoard();
            PlayerOne = new Player();
            PlayerTwo = new Player();
            PlayerList = new ObservableCollection<Player>();
            database = new ItemDatabase();

            Task.Run(async () => {
                await SetUpConnection();
            });
        }

        private async Task SetUpConnection() 
        {
            
            SignalR signalR = new SignalR();
            hubConnection = await signalR.ConnectBordHub();
      
            hubConnection.On<TicTacToe>(MethodEndPoint.SendBord, (tictactoe) =>
            {
                foreach (TicTacToe item in ticTacToeList)
                {
                    if (item.Index == tictactoe.Index)
                    {
                        item.GetSelectedText = tictactoe.GetSelectedText;
                        // Update Board
                    }
                }
            });

            await hubConnection.StartAsync();
        }

        private void SetupGameBoard()
        {
            //posible win´positions

            winPositions.Clear();

            winPositions.Add(new[] { 0, 1, 2 });
            winPositions.Add(new[] { 3, 4, 5 });
            winPositions.Add(new[] { 6, 7, 8 });

            winPositions.Add(new[] { 0, 3, 6 });
            winPositions.Add(new[] { 1, 4, 7 });
            winPositions.Add(new[] { 2, 5, 8 });

            winPositions.Add(new[] { 0, 4, 8 });
            winPositions.Add(new[] { 2, 4, 6 });

            //setting up the game

            ticTacToeList.Clear();
            ticTacToeList.Add(new TicTacToe(0));
            ticTacToeList.Add(new TicTacToe(1));
            ticTacToeList.Add(new TicTacToe(2));

            ticTacToeList.Add(new TicTacToe(3));
            ticTacToeList.Add(new TicTacToe(4));
            ticTacToeList.Add(new TicTacToe(5));

            ticTacToeList.Add(new TicTacToe(6));
            ticTacToeList.Add(new TicTacToe(7));
            ticTacToeList.Add(new TicTacToe(8));

            //binding the 9 boxes to the UI

        }

        [RelayCommand]
        public void RestartGame()
        {
            //todo adding score to the Heighscore view
            _playerTurn = 0;
            _isAnyoneWin = false;
            WinOrDrawText = "";
            //ResetPlayer();
            SetupGameBoard();
        }

        [RelayCommand]
        public async void SeclectedItem(TicTacToe selectedItem)
        {

            //no selicting after a win
            if (!string.IsNullOrWhiteSpace(selectedItem.GetSelectedText) || _isAnyoneWin) return;

            //logic for player 1 & 2
            if (_playerTurn == 0)
            {
                selectedItem.GetSelectedText = "X"; //player 1
            }
            else
            {
                selectedItem.GetSelectedText = "O"; //player 2
            }

            selectedItem.Player = _playerTurn;
            // swaps the player
            _playerTurn = _playerTurn == 0 ? 1 : 0;

            if (hubConnection != null) 
            {
                await hubConnection.SendAsync(MethodEndPoint.SendGame, selectedItem);
            } 

            CheckWinner();
        }

        private void CheckWinner()
        {
            // fetching the players index list
            var playerOneIndexList = ticTacToeList.Where(f => f.Player == 0).Select(f => f.Index).ToList();
            var playerTwoIndexList = ticTacToeList.Where(f => f.Player == 1).Select(f => f.Index).ToList();
            // her we checking for a possible winner
            if (playerOneIndexList.Count > 2 || playerTwoIndexList.Count > 2)
            {
                foreach (var win in winPositions)
                {
                    if (_isAnyoneWin) break;

                    int playerOneCount = 0;
                    int playerTwoCount = 0;

                    //player one check
                    foreach (var index in playerOneIndexList)
                    {
                        if (win.Contains(index))
                        {
                            playerOneCount++;
                        }
                        if (playerOneCount == 3)
                        {
                            PlayerOne.Win += 1;
                            PlayerTwo.Lose += 1;
                            Console.WriteLine("Wins: " + PlayerOne.Win);
                            WinOrDrawText = "Player 1 wins";
                            _isAnyoneWin = true;
                            break;
                        }
                    }

                    //Player two check
                    foreach (var index in playerTwoIndexList)
                    {
                        if (win.Contains(index))
                        {
                            playerTwoCount++;
                        }
                        if (playerTwoCount == 3)
                        {
                            PlayerTwo.Win += 1;
                            PlayerOne.Lose += 1;
                            WinOrDrawText = "Player 2 wins";
                            _isAnyoneWin = true;
                            break;
                        }
                    }
                }
            }
            // here ist when all frames a selected and now player has won the game
            if (ticTacToeList.Count(l => l.Player.HasValue) == 9 && !_isAnyoneWin)
            {
                WinOrDrawText = "it is a draw";
                PlayerOne.Draw += 1;
                PlayerTwo.Draw += 1;
                //Preferences.Set(WinOrDrawText, _isAnyoneWin); to set data in Sql Lite
            }
        }
        private async void AddHeighscore()
        {
            if (PlayerList.Where(p => p.Name == PlayerOne.Name).Count() != 0)
            {
                Player playerChange = PlayerList.Where(p => p.Name == PlayerOne.Name).First();
                playerChange.Win += PlayerOne.Win;
                playerChange.Draw += PlayerOne.Draw;
                playerChange.Lose += PlayerOne.Lose;
            }
            else
            {
                PlayerList.Add(PlayerOne);

            }

            if (PlayerList.Where(p => p.Name == PlayerTwo.Name).Count() != 0)
            {
                Player playerChange = PlayerList.Where(p => p.Name == PlayerTwo.Name).First();
                playerChange.Win += PlayerTwo.Win;
                playerChange.Draw += PlayerTwo.Draw;
                playerChange.Lose += PlayerTwo.Lose;
            }
            else
            {
                PlayerList.Add(PlayerTwo);
            }

            //await database.SaveResultAsync(PlayerOne);
            //await database.SaveResultAsync(PlayerOne);
        }

        [RelayCommand]
        public async Task GoToHeighscore()
        {
            AddHeighscore();
            //var convert = await database.GetPlayersAsync();
            //PlayerList = new ObservableCollection<Player>(convert);
            await Shell.Current.GoToAsync($"//HeighScore",true,
                new Dictionary<string, object>
                {
                    ["PlayerList"] = PlayerList
                });
            _playerTurn = 0;
            _isAnyoneWin = false;
            WinOrDrawText = "";
            SetupGameBoard();
        }
    }
}
