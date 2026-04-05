using SweebAppFront.ViewModels;

namespace SweebAppFront.Views;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
		BindingContext = new MainPageViewModel();
	}
}