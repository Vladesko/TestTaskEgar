using Application.Implementation.Validation;
using Application.Interfaces;
using Application.Models;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddApplicationServices();
            services.AddImplementations();
            return services;
        }
        private static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ISomeEntityService, SomeEntityService>();
            return services;
        }
        private static IServiceCollection AddImplementations(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CodeFilter>, FilterValidator>();
            return services;
        }
    }
}
