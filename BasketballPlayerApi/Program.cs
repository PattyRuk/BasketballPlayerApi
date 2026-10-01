
using BasketballPlayerApi.BLL.Services;
using BasketballPlayerApi.DAL;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BasketballPlayerApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<LeagueDbContext>(options =>
                options.UseInMemoryDatabase("BasketballLeagueDb"));

            // Dependency Injection lifecycle registrations
            builder.Services.AddScoped<ILeagueService, LeagueService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi(); // Exposes the native /openapi/v1.json spec file
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/openapi/v1.json", "Basketball League API v1");
                    options.RoutePrefix = "swagger"; // Access it at http://localhost:XXXX/swagger
                });
            }

            // Global Error Handling Setup 
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/problem+json"; // RFC 7807 standard header

                    var exceptionFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();
                    var exception = exceptionFeature?.Error;

                    var problemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
                    {
                        Status = 500,
                        Title = "An unhandled execution error occurred while processing your request.",
                        Detail = exception?.Message,
                        Instance = context.Request.Path
                    };

                    await context.Response.WriteAsJsonAsync(problemDetails);
                });
            });

            // 3. Routing and Core Pipeline Configurations
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}