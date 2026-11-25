using Microsoft.EntityFrameworkCore;
using task1_web_api.Data;
using task1_web_api.Interceptors;
using task1_web_api.Repositories;
using task1_web_api.Services;

namespace task1_web_api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<AuditInterceptor>();

            services.AddDbContext<AppDbContext>((serviceProvider, options) =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"));

                options.AddInterceptors(
                    serviceProvider.GetRequiredService<AuditInterceptor>());
            });

            services.AddControllers();
            services.AddScoped(
            typeof(IGenericRepository<>),
             typeof(GenericRepository<>));
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IPatientService, PatientService>();

            services.AddOpenApi();

            return services;
        }
    }
}