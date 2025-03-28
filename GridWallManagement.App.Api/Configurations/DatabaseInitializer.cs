using GridWallManagement.App.Api.Helpers;
using GridWallManagement.App.Database.DBContexts;

namespace GridWallManagement.App.Api.Configurations
{
    public static class DatabaseInitializer
    {
        public static void Initialize(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var env = services.GetRequiredService<Microsoft.AspNetCore.Hosting.IHostingEnvironment>();
                    var dbContext = services.GetRequiredService<DataBaseContext>();
                    DbInitializer.InitializeDatabase(dbContext, env);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }
        }
    }

}
