using GridWallManagement.App.Api.Configurations;

public class Program
{
    public static void Main(string[] args)
    {
        var webApplicationOptions = new WebApplicationOptions
        {
            ContentRootPath = AppContext.BaseDirectory,
            Args = args,
            ApplicationName = System.Diagnostics.Process.GetCurrentProcess().ProcessName
        };

        var builder = WebApplication.CreateBuilder(args);

        var app = ConfigureApplication(builder);

        app.Run();
    }

    private static WebApplication ConfigureApplication(WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        ServiceConfiguration.ConfigureServices(builder.Services, configuration);
        
        var app = builder.Build();
        DatabaseInitializer.Initialize(app);
        MiddlewareSetup.Configure(app);

        return app;
    }
}