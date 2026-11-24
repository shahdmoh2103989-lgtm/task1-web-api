using Microsoft.EntityFrameworkCore;
using task1_web_api.Data;
using task1_web_api.Mapping;

namespace task1_web_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Database Configuration
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

            // Controllers
            builder.Services.AddControllers();

            // AutoMapper
            builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile));

            // OpenAPI
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Logging
            var logger = app.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Health Care API Started Successfully");

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}