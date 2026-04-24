using SweebAppFront.ViewModels;

namespace SweebAppFront.Views;

public partial class CounterView : ContentView
{
	private readonly AlertsViewModel vm;
	public CounterView()
	{
		InitializeComponent();
		vm = new AlertsViewModel();
		BindingContext = vm;
	}

}