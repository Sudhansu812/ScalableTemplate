using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScalableApplication.Application.Interfaces.Repositories;
using ScalableApplication.Infrastructure.Persistence;
using ScalableApplication.Infrastructure.Repositories;

namespace ScalableApplication.Infrastructure.Extensions
{
    public static class InfrastructureDIExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("appDb") ?? throw new InvalidOperationException("Connection string not found.");
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
            
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();

            return services;
        }
    }
}
