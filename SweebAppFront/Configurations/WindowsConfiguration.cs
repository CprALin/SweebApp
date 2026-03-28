using Microsoft.Maui.Hosting;
using Microsoft.Maui.LifecycleEvents;

#if WINDOWS
using Microsoft.UI.Windowing;
using WinRT.Interop;
#endif

namespace SweebAppFront.Configurations;

public static class WindowsConfiguration
{
    public static MauiAppBuilder AddWindowsConfigurations(this MauiAppBuilder builder)
    {
#if WINDOWS
        builder.ConfigureLifecycleEvents(events =>
        {
            events.AddWindows(windows =>
            {
                windows.OnWindowCreated(window =>
                {
                    var hwnd = WindowNative.GetWindowHandle(window);
                    var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
                    var appWindow = AppWindow.GetFromWindowId(windowId);

                    WindowConfigurator.Configure(window);
                });
            });
        });
#endif

        return builder;
    }
}