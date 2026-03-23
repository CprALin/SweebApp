using System;
using System.Collections.Generic;
using System.Text;

namespace SweebAppFront.Configurations
{
    public static class ViewModelRegistration
    {
        public static IServiceCollection ViewModelRegistration(this IServiceCollection services)
        {
            // services.AddTransient<Page>();
            return services;
        }
    }
}
