using SweebAppFront.Enums;
using SweebAppFront.Services;
using SweebAppFront.ViewModels;
using System.Windows.Input;

namespace SweebAppFront.Views;

public partial class ThreatsView : ContentView
{
    private readonly ThreatsViewModel vm;
        
	public ThreatsView()
	{
		InitializeComponent();
        vm = new ThreatsViewModel();
        BindingContext = vm;

        SizeChanged += OnSizeChanged;
	}


    public void OnSizeChanged(object? sender, EventArgs e)
    {
        if(Width < 1500)
        {
            vm.Columns = 1;
        }else if(Width < 2500)
        {
            vm.Columns = 2;
        }
        else if(Width < 3500)
        {
            vm.Columns = 3;
        }
        else
        {
            vm.Columns = 4;
        }
    }

    private async void OnNotifPressed(object sender, TappedEventArgs e)
    {
        OverlayAlerts.IsVisible = true;

        PopupAlerts.Opacity = 0;
        PopupAlerts.TranslationY = -10;

        await Task.WhenAll(
            PopupAlerts.FadeToAsync(1, 120, Easing.CubicInOut),
            PopupAlerts.TranslateToAsync(0, 0, 180, Easing.CubicInOut)
        );
    }

    private async void OnCloseNotif(object sender, TappedEventArgs e)
    {
        await Task.WhenAll(
            PopupAlerts.FadeToAsync(0, 100, Easing.CubicInOut),
            PopupAlerts.TranslateToAsync(0, -8, 140, Easing.CubicInOut)
        );

        OverlayAlerts.IsVisible = false;
    }

    private async void OnPointerEntered(object sender, Microsoft.Maui.Controls.PointerEventArgs e)
    {
        if(sender is Border border)
        {
                border.Shadow = new Shadow
                {
                    Brush = Color.FromArgb("#3BC1A8"),
                    Offset = new Point(3, 3),
                    Radius = 2,
                    Opacity = 0.2f
                };
        }
    }

    private async void OnPointerExited(object sender, Microsoft.Maui.Controls.PointerEventArgs e)
    {
        if (sender is Border border)
        {
            border.Shadow = new Shadow
            {
                Opacity = 0
            };
        }
    }
}