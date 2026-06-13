using SweebAppFront.ViewModels;


namespace SweebAppFront.Views;

public partial class ThreatsView : ContentView
{
    private readonly ThreatsViewModel _vm;

    public ThreatsView(ThreatsViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = _vm;

        //_ = _vm.LoadThreats();
        SizeChanged += OnSizeChanged;
    }


    public void OnSizeChanged(object? sender, EventArgs e)
    {
        if (Width < 1500)
        {
            _vm.Columns = 1;
        }
        else if (Width < 2500)
        {
            _vm.Columns = 2;
        }
        else if (Width < 3500)
        {
            _vm.Columns = 3;
        }
        else
        {
            _vm.Columns = 4;
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