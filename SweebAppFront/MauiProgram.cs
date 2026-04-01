using Microsoft.Maui.LifecycleEvents;
using SweebAppFront.Configurations;

namespace SweebAppFront
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .AddAppFonts();

            builder.Services
                 .RegisterServices()
                 .RegisterViewModels()
                 .RegisterPages();

            return builder.Build();
        }
    }
}
