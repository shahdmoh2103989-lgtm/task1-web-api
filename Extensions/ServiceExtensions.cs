using Microsoft.EntityFrameworkCore;
using task1_web_api.Data;
using task1_web_api.Interceptors;
using task1_web_api.Repositories;
using task1_web_api.Services;
using task1_web_api.Filters;

namespace task1_web_api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Interceptor
            services.AddScoped<AuditInterceptor>();

            // DbContext
            services.AddDbContext<AppDbContext>((serviceProvider, options) =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"));

                options.AddInterceptors(
                    serviceProvider.GetRequiredService<AuditInterceptor>());
            });

            // Controllers
            services.AddControllers();

            // Generic Repository
            services.AddScoped(
                typeof(IGenericRepository<>),
                typeof(GenericRepository<>));

            // Patient Repository
            services.AddScoped<IPatientRepository, PatientRepository>();

            // Patient Service
            services.AddScoped<IPatientService, PatientService>();

            // Action Filter
            services.AddScoped<LogActionFilter>();

            // CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            // OpenAPI
            services.AddOpenApi();

            return services;
        }
    }
}