using SweebAppFront.ViewModels;

namespace SweebAppFront.Views;

public partial class DevicesView : ContentView
{
	DevicesViewModel vm;
	public DevicesView()
	{
		InitializeComponent();

		vm = new DevicesViewModel();
		BindingContext = vm;

		SizeChanged += OnSizeChanged;
	}

	void OnSizeChanged(object? sender, EventArgs e)
	{
		var width = Width;

		if(width < 1000)
		{
			vm.Columns = 1;
		}else if(width < 2000)
		{
			vm.Columns = 2;
		}
		else
		{
			vm.Columns = 3;
		}
	}
}