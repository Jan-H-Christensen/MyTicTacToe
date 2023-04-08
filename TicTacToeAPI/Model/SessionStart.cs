using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTicTacToe.MVVM.Model
{
    public class SessionStart : ObservableObject
    {
        public string GroupName { get; set; }
        public bool X_Or_O { get; set; }
        public TicTacToe? ticTacToe { get; set; }
    }
}
