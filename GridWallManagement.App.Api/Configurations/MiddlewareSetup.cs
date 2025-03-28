namespace GridWallManagement.App.Api.Configurations
{
    public static class MiddlewareSetup
    {

        public static void Configure(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GridWall Api v1"));
            }

            app.UseCors("AllowOrigin");
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }

}
