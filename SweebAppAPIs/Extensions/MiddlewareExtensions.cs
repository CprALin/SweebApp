
using SweebAppAPIs.Services;

namespace SweebAppAPIs.Extensions
{
    public static class MiddlewareExtensions
    {
        public static WebApplication UseAppPipeline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.MapHub<SignalRHub>("/signalRHub"); 

            app.UseCors("DefaultCors");

            return app;
        }
    }
}
