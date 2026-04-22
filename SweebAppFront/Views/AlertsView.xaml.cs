using SweebAppFront.ViewModels;

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

    private void SetCounter(object? sender, EventArgs e)
    {
        vm.CounterAlerts = 0;
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