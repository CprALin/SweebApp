using CommunityToolkit.Maui;
using LiveChartsCore.SkiaSharpView.Maui;
using SkiaSharp.Views.Maui.Controls.Hosting;
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
                .UseMauiCommunityToolkit()
                .UseSkiaSharp()
                .UseLiveCharts()
                .AddAppFonts();

            builder.Services
                 .RegisterPages()
                 .RegisterViewModels()
                 .RegisterServices();

            return builder.Build();
        }
    }
}
