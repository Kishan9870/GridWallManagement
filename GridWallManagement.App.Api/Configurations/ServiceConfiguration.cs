using AutoMapper.Extensions.ExpressionMapping;
using GridWallManagement.App.Database.DBContexts;
using GridWallManagement.App.Models;
using GridWallManagement.App.Services.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace GridWallManagement.App.Api.Configurations
{
    public static class ServiceConfiguration
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            ConfigureSwagger(services);
            ConfigureDatabase(services, configuration);
            services.AddHttpContextAccessor();
            services.RegisterService();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin", policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            services.AddAutoMapper(cfg =>
            {
                cfg.AddExpressionMapping();
            }, typeof(EntityToDTOMappingProfile).Assembly);

        }
        
        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "GridWallManagement Api",
                    Version = "v1",
                    License = new OpenApiLicense
                    {
                        Name = "GridWallManagement License",
                        Url = new Uri("https://example.com/license")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    swagger.IncludeXmlComments(xmlPath);
                }

                swagger.MapType<TimeSpan>(() => new OpenApiSchema
                {
                    Type = "string",
                    Format = "timespan(hh:mm:ss)",
                    Example = new OpenApiString("00:01:00")
                });

                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your token."
                });

                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
        }

        private static void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataBaseContext>(options =>
                options.UseMySql(configuration["DatabaseConnection:ConnectionString"], new MySqlServerVersion(new Version(8, 0, 21))));
        }
    }

}
