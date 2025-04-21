using GridWallManagement.App.Api.Middleware;

namespace GridWallManagement.App.Api.Configurations
{
    public static class MiddlewareSetup
    {

        public static void Configure(WebApplication app, IConfiguration configuration)
        {
            if (app.Environment.IsDevelopment() || app.Environment.IsProduction() || app.Environment.IsStaging())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GridWall Api v1"));
            }

            var myAllowSpecificOrigins = configuration.GetSection("MyAllowSpecificOrigins").Value;
            app.UseCors(myAllowSpecificOrigins);
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }

}
