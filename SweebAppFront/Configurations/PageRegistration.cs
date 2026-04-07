using SweebAppFront.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace SweebAppFront.Configurations
{
    public static class PageRegistration
    {
        public static IServiceCollection RegisterPages(this IServiceCollection services)
        {
            //Window
            services.AddSingleton<MainWindow>();
            
            //Pages
            services.AddTransient<MainPage>();
            services.AddTransient<LoginPage>();

            //Views
            services.AddTransient<DashboardView>();
            services.AddTransient<LiveRequestsView>();
            services.AddTransient<DevicesView>();
            services.AddTransient<RulesView>();
            services.AddTransient<ThreatsView>();

            return services;
        }
    }
}
