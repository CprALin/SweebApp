#if WINDOWS
using Microsoft.UI.Windowing;
using WinRT.Interop;
#endif

namespace SweebAppFront.Controls;

public partial class TitleBar : ContentView
{
    public TitleBar()
    {
        InitializeComponent();

        TitleBarRoot.HandlerChanged += (s, e) =>
        {
#if WINDOWS
            var mauiWindow = App.Current.Windows[0];
            var nativeWindow = mauiWindow.Handler.PlatformView as Microsoft.UI.Xaml.Window;

            var nativeView = TitleBarRoot.Handler.PlatformView as Microsoft.UI.Xaml.FrameworkElement;

            nativeWindow.SetTitleBar(nativeView); // 🔥 DRAG + TITLE BAR
#endif
        };
    }

    private void OnMinimize(object sender, EventArgs e)
    {
#if WINDOWS
        var hwnd = WindowNative.GetWindowHandle(App.Current.Windows[0].Handler.PlatformView);
        var id = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
        var appWindow = AppWindow.GetFromWindowId(id);

        if (appWindow.Presenter is OverlappedPresenter p)
            p.Minimize();
#endif
    }

    private void OnMaximize(object sender, EventArgs e)
    {
#if WINDOWS
        var hwnd = WindowNative.GetWindowHandle(App.Current.Windows[0].Handler.PlatformView);
        var id = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
        var appWindow = AppWindow.GetFromWindowId(id);

        if (appWindow.Presenter is OverlappedPresenter p)
        {
            if (p.State == OverlappedPresenterState.Maximized)
                p.Restore();
            else
                p.Maximize();
        }
#endif
    }

    private void OnClose(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }
}