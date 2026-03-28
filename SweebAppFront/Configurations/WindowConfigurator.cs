#if WINDOWS
using Microsoft.Maui.Controls;
using Microsoft.UI.Windowing;
using WinColor = Windows.UI.Color;
using WinRT.Interop;

namespace SweebAppFront.Configurations;

public static class WindowConfigurator
{
    public static void Configure(Microsoft.UI.Xaml.Window window)
    {
        var hwnd = WindowNative.GetWindowHandle(window);
        var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
        var appWindow = AppWindow.GetFromWindowId(windowId);

        ConfigureTitle(appWindow);
        ConfigurePresenter(appWindow);
        ConfigureTitleBar(appWindow);

        
    }

    private static void ConfigureTitle(AppWindow appWindow)
    {
        appWindow.Title = "SweebApp";
    }

    private static void ConfigurePresenter(AppWindow appWindow)
    {
        appWindow.SetPresenter(AppWindowPresenterKind.Overlapped);

        if (appWindow.Presenter is not OverlappedPresenter presenter)
            return;

        presenter.IsResizable = true;
        presenter.IsMaximizable = true;
        presenter.IsMinimizable = true;
        presenter.PreferredMinimumWidth = 1000;
        presenter.PreferredMinimumHeight = 700;
    }

    private static void ConfigureTitleBar(AppWindow appWindow)
    {
        if (!AppWindowTitleBar.IsCustomizationSupported())
            return;

        var variant = GetColor("Variant", "#0C7779");
        var white = WinColor.FromArgb(255, 255, 255, 255);

        var titleBar = appWindow.TitleBar;

        titleBar.ExtendsContentIntoTitleBar = false;

        // tot titlebar-ul pe Variant
        titleBar.BackgroundColor = variant;
        titleBar.ForegroundColor = white;

        titleBar.InactiveBackgroundColor = variant;
        titleBar.InactiveForegroundColor = white;

        // butoane
        titleBar.ButtonBackgroundColor = variant;
        titleBar.ButtonForegroundColor = white;

        titleBar.ButtonHoverBackgroundColor = Darken(variant, 0.10);
        titleBar.ButtonHoverForegroundColor = white;

        titleBar.ButtonPressedBackgroundColor = Darken(variant, 0.18);
        titleBar.ButtonPressedForegroundColor = white;

        titleBar.ButtonInactiveBackgroundColor = variant;
        titleBar.ButtonInactiveForegroundColor = white;
    }

    private static WinColor GetColor(string resourceKey, string fallbackHex)
    {
        if (Application.Current?.Resources.TryGetValue(resourceKey, out var value) == true &&
            value is Color mauiColor)
        {
            return WinColor.FromArgb(
                255,
                (byte)(mauiColor.Red * 255),
                (byte)(mauiColor.Green * 255),
                (byte)(mauiColor.Blue * 255));
        }

        return FromHex(fallbackHex);
    }

    private static WinColor FromHex(string hex)
    {
        hex = hex.Replace("#", "");

        byte a = 255;
        int start = 0;

        if (hex.Length == 8)
        {
            a = Convert.ToByte(hex[..2], 16);
            start = 2;
        }

        byte r = Convert.ToByte(hex.Substring(start, 2), 16);
        byte g = Convert.ToByte(hex.Substring(start + 2, 2), 16);
        byte b = Convert.ToByte(hex.Substring(start + 4, 2), 16);

        return WinColor.FromArgb(a, r, g, b);
    }

    private static WinColor Darken(WinColor color, double factor)
    {
        factor = Math.Clamp(factor, 0, 1);

        return WinColor.FromArgb(
            color.A,
            (byte)(color.R * (1 - factor)),
            (byte)(color.G * (1 - factor)),
            (byte)(color.B * (1 - factor)));
    }
}
#endif