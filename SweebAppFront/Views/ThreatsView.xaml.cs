using SweebAppFront.ViewModels;

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