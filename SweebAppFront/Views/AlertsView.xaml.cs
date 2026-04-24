using SweebAppFront.Models;
using SweebAppFront.ViewModels;
using System.Windows.Input;

namespace SweebAppFront.Views;

public partial class AlertsView : ContentView
{
    private readonly AlertsViewModel vm;
	public AlertsView()
	{
		InitializeComponent();
        vm = new AlertsViewModel();
        BindingContext = vm;
	}

    private async void OnPointerEntered(object sender, Microsoft.Maui.Controls.PointerEventArgs e)
    {
        if (sender is Border border)
        {
            border.Stroke = Color.FromArgb("#005461");
            border.StrokeThickness = 1;
        }
    }

    private async void OnPointerExited(object sender, Microsoft.Maui.Controls.PointerEventArgs e)
    {
        if (sender is Border border)
        {
            border.Stroke = null;
            border.StrokeThickness = 0;
        }
    }

}