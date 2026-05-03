using Microsoft.EntityFrameworkCore;
using SweebAppAPIs.Data;

namespace SweebAppAPIs.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite("Data Source=sweebapp.db"));

            return services;
        }
    }
}