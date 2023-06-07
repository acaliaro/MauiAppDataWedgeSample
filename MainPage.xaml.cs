using MauiAppDataWedgeSample.ViewModels;

namespace MauiAppDataWedgeSample;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

}

