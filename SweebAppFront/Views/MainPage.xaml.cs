namespace SweebAppFront.Views;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
		PageHost.Content = new HomePage();
	}
}