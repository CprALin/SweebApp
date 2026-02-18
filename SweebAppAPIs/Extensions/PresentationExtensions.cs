using Microsoft.AspNetCore.Mvc;

namespace SweebAppAPIs.Extensions
{
    public static class PresentationExtensions
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllers();
            services.AddOpenApi();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddCors(options =>
            {
                options.AddPolicy("DefaultCors", p =>
                    p.AllowAnyOrigin()
                     .AllowAnyHeader()
                     .AllowAnyMethod()
                );
            });

            return services;
        }
    }
}
