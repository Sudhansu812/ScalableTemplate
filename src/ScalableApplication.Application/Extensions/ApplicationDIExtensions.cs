using Microsoft.Extensions.DependencyInjection;
using ScalableApplication.Application.Features;
using ScalableApplication.Application.Interfaces.Services;

namespace ScalableApplication.Application.Extensions
{
    public static class ApplicationDIExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            return services;
        }
    }
}
