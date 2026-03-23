using Microsoft.Extensions.Logging;
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
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services
                 .RegisterServices()
                 .RegisterViewModels()
                 .RegisterPages();

            return builder.Build();
        }
    }
}
