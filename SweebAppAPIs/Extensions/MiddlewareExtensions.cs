

using Microsoft.EntityFrameworkCore;

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

            using(var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<Data.AppDbContext>();
                db.Database.Migrate();
            }

            app.UseHttpsRedirection();
            app.UseCors("DefaultCors");
            app.MapControllers();
           // app.MapHub<SignalRHub>("/signalRHub"); 


            return app;
        }
    }
}
