#if WINDOWS
using Microsoft.UI.Windowing;
using WinRT.Interop;
using WinColor = Windows.UI.Color;
#endif


namespace SweebAppFront;

public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
		Page = new Views.MainPage();
#if WINDOWS
	Created += OnCreated;
#endif
    }

#if WINDOWS
	private void OnCreated(object? sender, EventArgs e)
	{
		var nativeWindow = Handler?.PlatformView as Microsoft.UI.Xaml.Window;
		if (nativeWindow == null)
			return;

		var hwnd = WindowNative.GetWindowHandle(nativeWindow);
		var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
		var appWindow = AppWindow.GetFromWindowId(windowId);
		var titleBar = appWindow.TitleBar;

		var accent = GetWinColor("Accent");         
		var text = GetWinColor("TextPrimary");

		// hover & pressed
		var hover = Darken(accent, 0.15);   
		var pressed = Darken(accent, 0.30); 

		titleBar.ButtonBackgroundColor = accent;
		titleBar.ButtonForegroundColor = text;

		titleBar.ButtonHoverBackgroundColor = hover;
		titleBar.ButtonHoverForegroundColor = text;

		titleBar.ButtonPressedBackgroundColor = pressed;
		titleBar.ButtonPressedForegroundColor = text;

		titleBar.ButtonInactiveBackgroundColor = accent;
		titleBar.ButtonInactiveForegroundColor = text;

		titleBar.ForegroundColor = text;
		titleBar.InactiveForegroundColor = text;

	}

	private static WinColor GetWinColor(string key)
	{
		if (Application.Current?.Resources.TryGetValue(key, out var value) == true &&
			value is Color c)
		{
			return WinColor.FromArgb(
				(byte)(c.Alpha * 255),
				(byte)(c.Red * 255),
				(byte)(c.Green * 255),
				(byte)(c.Blue * 255));
		}

		return WinColor.FromArgb(255, 0, 0, 0);
	}

	private static WinColor Darken(WinColor color, double factor)
	{
		factor = Math.Clamp(factor, 0, 1);

		return WinColor.FromArgb(
			color.A,
			(byte)(color.R * (1 - factor)),
			(byte)(color.G * (1 - factor)),
			(byte)(color.B * (1 - factor))
		);
	}
#endif
}