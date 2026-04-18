using SweebAppFront.Services.Interfaces;
using SweebAppFront.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace SweebAppFront.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IServiceProvider _provider;

        public NavigationService(IServiceProvider provider)
        {
            _provider = provider;
        }

        public View GetView(string key)
        {
            return key switch
            {
                "Dashboard" => _provider.GetRequiredService<DashboardView>(),
                "LiveRequests" => _provider.GetRequiredService<LiveRequestsView>(),
                "Threats" => _provider.GetRequiredService<ThreatsView>(),
                _ => _provider.GetRequiredService<DashboardView>()
            };
        }
    }
}
