using SweebAppFront.ViewModels;

namespace SweebAppFront.Views;

public partial class LiveRequestsView : ContentView
{
	public LiveRequestsView(LiveRequestsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }
}