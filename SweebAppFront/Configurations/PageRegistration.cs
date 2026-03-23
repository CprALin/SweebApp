using System;
using System.Collections.Generic;
using System.Text;

namespace SweebAppFront.Configurations
{
    public static class PageRegistration
    {
        public static IServiceCollection RegisterPages(this IServiceCollection services)
        {
            // services.AddTransient<Page>();
            return services;
        }
    }
}
