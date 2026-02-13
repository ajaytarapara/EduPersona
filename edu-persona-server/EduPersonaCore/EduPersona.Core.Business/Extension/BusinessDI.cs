using EduPersona.Core.Business.IServices;
using EduPersona.Core.Business.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EduPersona.Core.Business.Extension
{
    public static class BusinessDI
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var serviceTypes = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Service"));

            foreach (var implementation in serviceTypes)
            {
                var interfaceType = implementation.GetInterfaces()
                    .FirstOrDefault(i => i.Name == $"I{implementation.Name}");

                if (interfaceType != null)
                {
                    services.AddScoped(interfaceType, implementation);
                }
            }

            return services;
        }
    }
}
