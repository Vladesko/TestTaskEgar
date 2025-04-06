using Application.Interfaces;
using Infastructure.DataBase;
using Infastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infastructure.Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDataBase(configuration);
            services.AddRepositories();

            return services;
        }
        /// <summary>
        /// Добавляет репозитории для уровня Infastructure
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ISomeEntitiesRepository, SomeEntityRepository>();
            return services;
        }
        /// <summary>
        /// Добавляет контекст для БД
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration">Нужен для строки соединения с БД</param>
        /// <returns></returns>
        private static IServiceCollection AddDataBase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SomeEntityDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });
            return services;
        }
    }
}
