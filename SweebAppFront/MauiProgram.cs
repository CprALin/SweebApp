using Microsoft.Maui.LifecycleEvents;
using SweebAppFront.Configurations;
#if WINDOWS
using WinRT.Interop;
using SweebAppFront.Platforms.Windows;
#endif

namespace SweebAppFront
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services
                 .RegisterServices()
                 .RegisterViewModels()
                 .RegisterPages();

            builder.ConfigureLifecycleEvents(events =>
            {
#if WINDOWS
                events.AddWindows(w =>
                {
                    w.OnWindowCreated(window =>
                    {
                        var hwnd = WindowNative.GetWindowHandle(window);
                        
                       
                    });
                });
#endif
            });

            return builder.Build();
        }
    }
}
