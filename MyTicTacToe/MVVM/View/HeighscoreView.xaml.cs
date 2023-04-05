using MyTicTacToe.MVVM.ViewModel;
namespace MyTicTacToe.MVVM.View;

public partial class HeighscoreView : ContentPage
{
	public HeighscoreView(HeighscoreViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}