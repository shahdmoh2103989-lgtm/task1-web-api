using task1_web_api.Extensions;
using task1_web_api.Mapping;

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

            // Logging
            var logger = app.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation(
                "Health Care API Started Successfully");

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