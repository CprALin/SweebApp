using Microsoft.EntityFrameworkCore;
using SweebAppAPIs.Data;
using SweebAppAPIs.Data.Repositories;

namespace SweebAppAPIs.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("SqlConnection")));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRuleRepository , RuleRepository>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<IAlertRepository , AlertRepository>();
            services.AddScoped<IThreatRepository , ThreatRepository>();
            return services;
        }
    }
}