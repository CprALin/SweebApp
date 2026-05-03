using Microsoft.OpenApi;
using System.Text;


namespace SweebAppAPIs.Extensions
{
    public static class PresentationExtensions
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddCors(options =>
            {
                options.AddPolicy("DefaultCors", p =>
                    p.WithOrigins("https://localhost:7832",
                                  "http://localhost:7432")
                     .AllowAnyHeader()
                     .AllowAnyMethod()
                );
            });

            services.AddSignalR();

            return services;
        }
    }
}
