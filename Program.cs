using task1_web_api.Extensions;
using task1_web_api.Mapping;
using task1_web_api.Middlewares;

namespace task1_web_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Application Services
            builder.Services.AddApplicationServices(
                builder.Configuration);

            // AutoMapper
            builder.Services.AddAutoMapper(
                cfg => { }, typeof(MappingProfile));

            var app = builder.Build();

            // Custom Middleware
            app.UseMiddleware<RequestLoggingMiddleware>();

            // Logging
            var logger = app.Services.GetRequiredService<ILogger<Program>>();

            logger.LogInformation(
                "Health Care API Started Successfully");

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            // HTTPS Redirection
            app.UseHttpsRedirection();

            // CORS
            app.UseCors("AllowFrontend");

            // Authorization
            app.UseAuthorization();

            // Controllers
            app.MapControllers();

            app.Run();
        }
    }
}