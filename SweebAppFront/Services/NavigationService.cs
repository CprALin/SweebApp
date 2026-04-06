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
                "Devices" => _provider.GetRequiredService<DevicesView>(),
                "Rules" => _provider.GetRequiredService<RulesView>(),
                "Threats" => _provider.GetRequiredService<ThreatsView>(),
                _ => _provider.GetRequiredService<DashboardView>()
            };
        }
    }
}
