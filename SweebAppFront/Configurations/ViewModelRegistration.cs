using SweebAppFront.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SweebAppFront.Configurations
{
    public static class ViewModelRegistration
    {
        public static IServiceCollection RegisterViewModels(this IServiceCollection services)
        {
            services.AddSingleton<MainPageViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddSingleton<LiveRequestsViewModel>();
            services.AddSingleton<DashboardViewModel>();
            services.AddSingleton<AlertsViewModel>();
            services.AddSingleton<ThreatsViewModel>();

            return services;
        }
    }
}
