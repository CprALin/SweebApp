

using SweebAppAPIs.Data;
using SweebAppAPIs.Data.Repositories;
using SweebAppAPIs.Data.Repositories.Interfaces;
using SweebAppAPIs.Services;
using SweebAppAPIs.Services.Interfaces;

namespace SweebAppAPIs.Extensions
{
	public static class ApplicationExtensions
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
            //Repositories
            services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IDeviceRepository, DeviceRepository>();
			services.AddScoped<IThreatRepository, ThreatRepository>();
			services.AddScoped<IAlertRepository, AlertRepository>();

            //Services
            services.AddScoped<IUserServices, UserServices>();
			services.AddScoped<IDeviceServices, DeviceServices>();
			services.AddScoped<IThreatServices, ThreatServices>();
			services.AddScoped<IAlertsServices, AlertsServices>();

            return services;
		}
	}
}
