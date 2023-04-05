using CommunityToolkit.Mvvm.ComponentModel;

namespace MyTicTacToe.MVVM.Model
{
    public partial class TicTacToe : ObservableObject
    {
        public TicTacToe(int index) 
        {   
            this.Index = index;
        }
        public int Index { get; set; }

        [ObservableProperty]
        private string getSelectedText;

        /// <summary>
        /// player 0 = x
        /// player 1 = o
        /// </summary>
        public int? Player { get; set; } 
    }
}
