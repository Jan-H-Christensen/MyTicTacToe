using MyTicTacToe.MVVM.ViewModel;

namespace MyTicTacToe.MVVM.View;

public partial class TicTacView : ContentPage
{
	public TicTacView(TicTacToeViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}