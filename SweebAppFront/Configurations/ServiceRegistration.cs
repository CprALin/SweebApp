using SweebAppFront.Services;
using SweebAppFront.Services.Interfaces;
using SweebAppFront.ViewModels;
using SweebAppFront.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace SweebAppFront.Configurations
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<IAuthStateService, AuthStateService>();
            services.AddSingleton<INavigationService, NavigationService>();

            return services;
        }
    }
}
