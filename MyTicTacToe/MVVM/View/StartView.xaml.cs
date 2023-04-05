using MyTicTacToe.MVVM.ViewModel;
namespace MyTicTacToe.MVVM.View;

public partial class StartView : ContentPage
{
	public StartView(StartViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}