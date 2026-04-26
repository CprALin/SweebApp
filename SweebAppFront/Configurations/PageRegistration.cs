using SweebAppFront.Components;
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
            services.AddTransient<ThreatsView>();
            services.AddTransient<LabelFilter>();
            services.AddTransient<MenuButton>();
            services.AddTransient<SlidebarView>();
            services.AddTransient<AlertsView>();
            services.AddTransient<CounterView>();
            services.AddTransient<ThreatsFilterView>();

            return services;
        }
    }
}
