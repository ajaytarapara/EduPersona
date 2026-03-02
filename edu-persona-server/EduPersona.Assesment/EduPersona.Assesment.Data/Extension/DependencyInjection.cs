using EduPersona.Assesment.Data.IRepositories;
using EduPersona.Assesment.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EduPersona.Assesment.Data.Extension
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}