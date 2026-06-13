

using Microsoft.EntityFrameworkCore;
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

            using(var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<Data.AppDbContext>();
                db.Database.Migrate();
                EnsureSqliteSchema(db);
            }

            app.UseHttpsRedirection();
            app.UseCors("DefaultCors");
            app.MapControllers();
            app.MapHub<SignalRHub>("/signalRHub"); 


            return app;
        }

        private static void EnsureSqliteSchema(Data.AppDbContext db)
        {
            db.Database.ExecuteSqlRaw("""
                CREATE TABLE IF NOT EXISTS "Alerts" (
                    "Id" INTEGER NOT NULL CONSTRAINT "PK_Alerts" PRIMARY KEY AUTOINCREMENT,
                    "Message" TEXT NOT NULL,
                    "Severity" INTEGER NOT NULL,
                    "ThreatEventId" INTEGER NOT NULL,
                    "CreatedAt" TEXT NOT NULL
                );
                """);

            db.Database.ExecuteSqlRaw("""
                CREATE TABLE IF NOT EXISTS "Device" (
                    "Id" INTEGER NOT NULL CONSTRAINT "PK_Device" PRIMARY KEY AUTOINCREMENT,
                    "Name" TEXT NOT NULL,
                    "OS" TEXT NOT NULL,
                    "CreatedAt" TEXT NOT NULL,
                    "UserId" INTEGER NOT NULL
                );
                """);

            db.Database.ExecuteSqlRaw("""
                CREATE TABLE IF NOT EXISTS "ThreatEvents" (
                    "Id" INTEGER NOT NULL CONSTRAINT "PK_ThreatEvents" PRIMARY KEY AUTOINCREMENT,
                    "URL" TEXT NOT NULL,
                    "Protocol" TEXT NOT NULL,
                    "Timestamp" TEXT NOT NULL,
                    "Verdict" TEXT NOT NULL,
                    "ActionTaken" INTEGER NOT NULL,
                    "Score" REAL NOT NULL,
                    "Category" TEXT NOT NULL,
                    "DeviceId" INTEGER NOT NULL
                );
                """);

            db.Database.ExecuteSqlRaw("""
                CREATE TABLE IF NOT EXISTS "User" (
                    "Id" INTEGER NOT NULL CONSTRAINT "PK_User" PRIMARY KEY AUTOINCREMENT,
                    "UserName" TEXT NOT NULL
                );
                """);
        }
    }
}
