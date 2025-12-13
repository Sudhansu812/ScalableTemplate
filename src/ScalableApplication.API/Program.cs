using dotenv.net;
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
            DotEnv.Load();
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApiVersioning();
            builder.Services.AddControllers().AddNewtonsoftJson();
            builder.Services.AddOpenApi();

            var app = builder.Build();
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
