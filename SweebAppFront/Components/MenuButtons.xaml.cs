using Microsoft.Maui.Controls.PlatformConfiguration;

namespace SweebAppFront.Components;

public partial class MenuButtons : ContentView
{

	public static readonly BindableProperty IconPathProperty = BindableProperty.Create(
		nameof(IconPath),
		typeof(string),
		typeof(MenuButtons),
		string.Empty
	);

    public static readonly BindableProperty MenuTextProperty = BindableProperty.Create(
		nameof(MenuText),
		typeof(string),
		typeof(MenuButtons),
		string.Empty
	);

	public static readonly BindableProperty SetIconWidthProperty = BindableProperty.Create(
		nameof(SetIconWidth),
		typeof(double),
		typeof(MenuButtons),
		defaultValue: 30.0
	);

	public static readonly BindableProperty SetIconHeightProperty = BindableProperty.Create(
		nameof(SetIconHeight),
		typeof(double),
		typeof(MenuButtons),
		defaultValue: 30.0

    );

	public static readonly BindableProperty SetButtonRadiusProperty = BindableProperty.Create(
		nameof(SetButtonRadius),
		typeof(float),
		typeof(MenuButtons),
		defaultValue: 5.0f
    );

	public static readonly BindableProperty SetTextFontSizeProperty = BindableProperty.Create(
		nameof(SetTextFontSize),
		typeof(string),
		typeof(MenuButtons),
		defaultValue: "Medium"
    );

	public string SetTextFontSize
	{
		get => (string)GetValue(SetTextFontSizeProperty);
		set => SetValue(SetTextFontSizeProperty, value);
    }

    public double SetButtonRadius
	{
		get => (float)GetValue(SetButtonRadiusProperty);
		set => SetValue(SetButtonRadiusProperty, value);
    }
    public double SetIconWidth
	{
		get => (double)GetValue(SetIconWidthProperty);
		set => SetValue(SetIconWidthProperty, value);
    }

	public double SetIconHeight
	{
		get => (double)GetValue(SetIconHeightProperty);
		set => SetValue(SetIconHeightProperty, value);
    }

    public string IconPath
	{
		get => (string)GetValue(IconPathProperty);
		set => SetValue(IconPathProperty, value);
	}

	public string MenuText
	{
		get => (string)GetValue(MenuTextProperty);
		set => SetValue(MenuTextProperty, value);
    }

    public MenuButtons()
	{
        InitializeComponent();
    }
}