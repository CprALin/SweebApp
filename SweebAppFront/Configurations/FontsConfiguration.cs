using System;
using System.Collections.Generic;
using System.Text;

namespace SweebAppFront.Configurations
{
    public static class FontsConfiguration
    {
        public static MauiAppBuilder AddAppFonts(this MauiAppBuilder builder)
        {
            builder.ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

            return builder;
        }       
    }
}
