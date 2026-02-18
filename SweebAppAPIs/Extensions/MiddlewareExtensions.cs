using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

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
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.UseCors("DefaultCors");

            return app;
        }
    }
}
