using SweebAppFront.Models;
using SweebAppFront.ViewModels;
using System.Windows.Input;

namespace SweebAppFront.Views;

public partial class AlertsView : ContentView
{
	public AlertsView()
	{
		InitializeComponent();
        BindingContext = new AlertsViewModel();
	}

	private async void OnPointerEntered(object sender, PointerEventArgs e)
	{
		if(sender is Border border)
		{
            border.Stroke = Color.FromArgb("#F1EFEC");
            border.StrokeThickness = 2;
        }
	}

	private async void OnPointerExited(object sender, PointerEventArgs e)
	{
		if(sender is Border border)
		{
            border.Stroke = Colors.Transparent;
            border.StrokeThickness = 1;
        }
	}
}