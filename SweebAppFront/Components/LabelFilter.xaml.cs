using System.Windows.Input;

namespace SweebAppFront.Components;

public partial class LabelFilter : ContentView
{
	public LabelFilter()
	{
		InitializeComponent();
	}

    public string LabelText
    {
        get => (string)GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }

    public static readonly BindableProperty LabelTextProperty =
    BindableProperty.Create(nameof(LabelText), typeof(string), typeof(LabelFilter), string.Empty);

    public string FilterKey
    {
        get => (string)GetValue(FilterKeyProperty);
        set => SetValue(FilterKeyProperty, value);
    }

    public static readonly BindableProperty FilterKeyProperty =
    BindableProperty.Create(nameof(FilterKey), typeof(string), typeof(LabelFilter), "", propertyChanged : OnStateChange);
    public string CurrentFilterKey
    {
        get => (string)GetValue(CurrentFilterKeyProperty);
        set => SetValue(CurrentFilterKeyProperty, value);
    }

    public static readonly BindableProperty CurrentFilterKeyProperty =
    BindableProperty.Create(nameof(CurrentFilterKey), typeof(string), typeof(LabelFilter), "", propertyChanged : OnStateChange);

    private static void OnStateChange(BindableObject bindable, object oldValue, object newValue)
    {
        if(bindable is not LabelFilter view)
            return;

        bool isActive = view.FilterKey == view.CurrentFilterKey;
        
   
        view.FilterBorder.BackgroundColor = isActive ? Color.FromArgb("#005461") : Colors.Transparent; 
        
    }

    public ICommand Command 
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public static readonly BindableProperty CommandProperty =
    BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(LabelFilter));

    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    public static readonly BindableProperty CommandParameterProperty =
    BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(LabelFilter));

    private void OnLabelTapped(object sender, TappedEventArgs e)
    {

        if(Command?.CanExecute(CommandParameter) == true)
        {
            Command.Execute(CommandParameter);
        }
    }

    private void OnPointerEnter(object sender, PointerEventArgs e)
    {
        FilterBorder.BackgroundColor = Color.FromArgb("#005461");
        FilterBorder.Scale = 1.05;
    }

    private void OnPointerExited(object sender, PointerEventArgs e)
    {
        FilterBorder.BackgroundColor = Colors.Transparent;
        FilterBorder.Scale = 1.0;
    }
}