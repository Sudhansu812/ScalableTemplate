using dotenv.net;
using ScalableApplication.API.Extensions;
using ScalableApplication.API.Middleware;
using ScalableApplication.Application.Extensions;
using ScalableApplication.Infrastructure.Extensions;
using Scalar.AspNetCore;

namespace ScalableApplication.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            DotEnv.Load(options: DotEnv.Fluent().WithEnvFiles("../../env/.env"));
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddScalableAppApiVersioning();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend",
                    policy =>
                    {
                        policy
                        .WithOrigins("http://localhost:5173")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });
            builder.Services.AddControllers().AddNewtonsoftJson();
            builder.Services.AddOpenApi();

            var app = builder.Build();
            app.UseCors("AllowFrontend");
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
