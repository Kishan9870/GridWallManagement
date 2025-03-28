using GridWallManagement.App.Database.DBContexts;

namespace GridWallManagement.App.Api.Helpers
{
    public class DbInitializer
    {
        public static void InitializeDatabase(DataBaseContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            context.Database.EnsureCreated();

            //if (env.IsDevelopment())
            //    context.Database.Migrate();

            //Initialize your tables here
            //SeedDataForRoles(context);
        }
    }
}
