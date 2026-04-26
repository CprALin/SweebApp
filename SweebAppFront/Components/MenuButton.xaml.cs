

using System.Windows.Input;

namespace SweebAppFront.Components;


public partial class MenuButton : ContentView
{
    public MenuButton()
    {
        InitializeComponent();
    }

    public string PageKey
    {
        get => (string)GetValue(PageKeyProperty);
        set => SetValue(PageKeyProperty, value);
    }

    public string CurrentPageKey
    {
        get => (string)GetValue(CurrentPageKeyProperty);
        set => SetValue(CurrentPageKeyProperty, value);
    }

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

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public object CommandParam
    {
        get => GetValue(CommandParamProperty);
        set => SetValue(CommandParamProperty, value);
        
    }

    public static readonly BindableProperty IconPathProperty = 
        BindableProperty.Create(
            nameof(IconPath),
            typeof(string),
            typeof(MenuButton),
            string.Empty
        );

    public static readonly BindableProperty MenuTextProperty = 
        BindableProperty.Create(
            nameof(MenuText),
            typeof(string),
            typeof(MenuButton),
            string.Empty
        );

    public static readonly BindableProperty SetIconWidthProperty = 
        BindableProperty.Create(
            nameof(SetIconWidth),
            typeof(double),
            typeof(MenuButton),
            defaultValue: 30.0
        );

    public static readonly BindableProperty SetIconHeightProperty = 
        BindableProperty.Create(
            nameof(SetIconHeight),
            typeof(double),
            typeof(MenuButton),
            defaultValue: 30.0

        );

    public static readonly BindableProperty SetButtonRadiusProperty = 
        BindableProperty.Create(
            nameof(SetButtonRadius),
            typeof(float),
            typeof(MenuButton),
            defaultValue: 5.0f
        );

    public static readonly BindableProperty SetTextFontSizeProperty = 
        BindableProperty.Create(
            nameof(SetTextFontSize),
            typeof(string),
            typeof(MenuButton),
            defaultValue: "Medium"
        );

    public static readonly BindableProperty PageKeyProperty =
    BindableProperty.Create(nameof(PageKey), typeof(string), typeof(MenuButton), "", propertyChanged: OnStateChanged);

    public static readonly BindableProperty CurrentPageKeyProperty =
        BindableProperty.Create(nameof(CurrentPageKey), typeof(string), typeof(MenuButton), "", propertyChanged: OnStateChanged);

    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(MenuButton));

    public static readonly BindableProperty CommandParamProperty =
        BindableProperty.Create(nameof(CommandParam), typeof(object), typeof(MenuButton));

    private static void OnStateChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not MenuButton view)
            return;

        bool isActive = view.PageKey == view.CurrentPageKey;

        view.MenuButtonBorder.Stroke = isActive
            ? new SolidColorBrush(Color.FromArgb("#005461"))
            : new SolidColorBrush(Colors.Transparent);
        view.MenuButtonBorder.StrokeThickness = isActive ? 2 : 0;
    }

    private void OnTapped(object sender, TappedEventArgs e)
    {
        if (Command?.CanExecute(CommandParam) == true)
            Command.Execute(CommandParam);
    }

    private async void OnPointerEntered(object sender, Microsoft.Maui.Controls.PointerEventArgs e)
    {
        MenuButtonBorder.Shadow = new Shadow
        {
            Brush = Color.FromArgb("#005461"),
            Offset = new Point(5, 5),
            Radius = 5,
            Opacity = 0.5f
        };
    }

    private async void OnPointerExited(object sender, Microsoft.Maui.Controls.PointerEventArgs e)
    {
        MenuButtonBorder.Shadow = new Shadow
        {
            Opacity = 0
        };
    }
}