using EduPersona.Core.Data.IRepositories;
using EduPersona.Core.Data.Repositories;
using EduPersona.Core.Data.Repositories.EduPersona.Core.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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
