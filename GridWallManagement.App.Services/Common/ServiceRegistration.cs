using GridWallManagement.App.Repository.Common;
using GridWallManagement.App.Services.Abstract;
using GridWallManagement.App.Services.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace GridWallManagement.App.Services.Common
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterService(this IServiceCollection services)
        {
            services.AddScoped<ILicenseKeyService, LicenseKeyService>();
            services.AddScoped<IUserLicenseService, UserLicenseService>();
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<IUsersService, UsersService>();

            services.RegisterRepository();
            return services;
        }
    }
}
