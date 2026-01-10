using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScalableApplication.Application.Helpers;
using ScalableApplication.Application.Interfaces.v1.Repositories;
using ScalableApplication.Infrastructure.Persistence;
using ScalableApplication.Infrastructure.Repositories.v1;
using System.Text;

namespace ScalableApplication.Infrastructure.Extensions
{
    public static class InfrastructureDIExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            string encryptedConnectionString = configuration.GetConnectionString("appDb") ?? throw new InvalidOperationException("Connection string not found.");
            string encryptedConnectionStringTag = configuration.GetConnectionString("appDbTag") ?? throw new InvalidOperationException("Connection string not found.");
            string connectionString = Cryptography.Decrypt(encryptedConnectionString, Convert.FromBase64String(encryptedConnectionStringTag), Encoding.UTF8.GetBytes("ConnectionString:appDb"));
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
            
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();

            return services;
        }
    }
}
