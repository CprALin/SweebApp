namespace SweebAppFront.Views;

public partial class MainPage : ContentPage
{
	private readonly HomePage _homePage = new();
	public MainPage()
	{
		InitializeComponent();
		PageHost.Content = _homePage;
	}
}