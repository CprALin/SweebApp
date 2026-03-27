#if WINDOWS
using Microsoft.UI.Windowing;
using WinRT.Interop;
#endif

namespace SweebAppFront;

public static class WindowCommands
{
    public static void Minimize()
    {
#if WINDOWS
        var platformWindow = App.Current!.Windows[0].Handler!.PlatformView;
        var hwnd = WindowNative.GetWindowHandle(platformWindow);
        var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
        var appWindow = AppWindow.GetFromWindowId(windowId);

        if (appWindow.Presenter is OverlappedPresenter p)
            p.Minimize();
#endif
    }

    public static void ToggleMaximize()
    {
#if WINDOWS
        var platformWindow = App.Current!.Windows[0].Handler!.PlatformView;
        var hwnd = WindowNative.GetWindowHandle(platformWindow);
        var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
        var appWindow = AppWindow.GetFromWindowId(windowId);

        if (appWindow.Presenter is OverlappedPresenter p)
        {
            if (p.State == OverlappedPresenterState.Maximized)
                p.Restore();
            else
                p.Maximize();
        }
#endif
    }

    public static void Close()
    {
        Application.Current?.Quit();
    }
}