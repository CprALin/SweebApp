using SweebAppFront.Configurations;
using CommunityToolkit.Maui;

namespace SweebAppFront
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .AddAppFonts();

            builder.Services
                 .RegisterPages()
                 .RegisterViewModels()
                 .RegisterServices();

            return builder.Build();
        }
    }
}
