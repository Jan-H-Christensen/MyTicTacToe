using Microsoft.Extensions.Logging;
using MyTicTacToe.MVVM.View;
using MyTicTacToe.MVVM.ViewModel;

namespace MyTicTacToe;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<StartViewModel>();
		builder.Services.AddSingleton<TicTacToeViewModel>();
		builder.Services.AddSingleton<HeighscoreViewModel>();

		builder.Services.AddTransient<StartView>();
		builder.Services.AddTransient<TicTacView>();
		builder.Services.AddTransient<HeighscoreView>();

		return builder.Build();
	}
}
