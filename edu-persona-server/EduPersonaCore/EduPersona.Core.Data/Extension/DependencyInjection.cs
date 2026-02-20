using EduPersona.Core.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EduPersona.Core.Data.Extension
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
