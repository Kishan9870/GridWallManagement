using Microsoft.Extensions.DependencyInjection;

namespace GridWallManagement.App.Repository.Common
{
    public static class RepositoryRegistration
    {
        public static IServiceCollection RegisterRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}
