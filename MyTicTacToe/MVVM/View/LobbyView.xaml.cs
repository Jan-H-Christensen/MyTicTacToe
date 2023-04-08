using MyTicTacToe.MVVM.ViewModel;
namespace MyTicTacToe.MVVM.View;

public partial class LobbyView : ContentPage
{
	public LobbyView(LobbyViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}