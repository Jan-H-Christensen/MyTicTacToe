namespace MyTicTacToe;
using MVVM.View;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(StartView), typeof(StartView));
        Routing.RegisterRoute(nameof(TicTacView), typeof(TicTacView));
        Routing.RegisterRoute(nameof(HeighscoreView), typeof(HeighscoreView));
    }
}
