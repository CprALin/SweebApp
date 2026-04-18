using SweebAppFront.ViewModels;

namespace SweebAppFront.Views;

public partial class LiveRequestsView : ContentView
{
	public LiveRequestsView()
	{
		InitializeComponent();
		BindingContext = new LiveRequestsViewModel();
    }
}