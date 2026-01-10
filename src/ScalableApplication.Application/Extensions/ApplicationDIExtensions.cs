using Microsoft.Extensions.DependencyInjection;
using ScalableApplication.Application.Features.v1;
using ScalableApplication.Application.Interfaces.v1.Services;

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
