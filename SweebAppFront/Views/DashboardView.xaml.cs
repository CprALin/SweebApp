using SweebAppFront.ViewModels;

namespace SweebAppFront.Views;

public partial class DashboardView : ContentView
{
	public DashboardView(DashboardViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}
