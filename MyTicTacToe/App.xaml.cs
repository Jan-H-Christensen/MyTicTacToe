using MyTicTacToe.MVVM.View;

namespace MyTicTacToe;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		// makes the tic tac toe view to the main page
		MainPage = new AppShell();
	}
}
