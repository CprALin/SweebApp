using SweebAppFront.ViewModels;

namespace SweebAppFront.Views;

public partial class ThreatsFilterView : ContentView
{
	public ThreatsFilterView()
	{
		InitializeComponent();
		BindingContext = new ThreatsFilterViewModel();
	}
}