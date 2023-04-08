using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTicTacToe.MVVM.Model
{
    internal class SessionControler
    {
        public string GroupName { get; set; }
        public bool Turn { get; set; }
        public TicTacToe? ticTacToe { get; set; }
    }
}
